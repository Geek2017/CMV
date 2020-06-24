using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using SignalRHubWebApi.Models;
using SignalRHubWebApi.SFServiceReference;
using System.Web;
using SignalRHubWebApi;
using System.Collections.Generic;
using Newtonsoft.Json;
using SignalRHubWebApi.Resources;

namespace SignalRHubWebApi.Hubs
{
    public class MyHub : Hub
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MyHub));

        public string _apiEndPoint = System.Configuration.ConfigurationSettings.AppSettings.Get("ApiEndPoint").ToString();
        #region properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthToken { get; set; }
        public string InstanceUrl { get; set; }
        #endregion properties

        //Send request to unique client on the basis of ExtensionNo/CustomerNo
        public void SendToUniqueClient(string connectionId, string salesforceUrl,
             string customerId, string extensionNo, string phoneNo)

        {
            Random randomId = new Random();
            string decryptedPass = string.Empty;
            string passFromDb = string.Empty;
            string CRMAppendId = string.Empty;
            string CRMUrl = string.Empty;
            int randomLogId = randomId.Next();

            logger.Info(string.Format("SequenceId {0} : ---------------------------- SendToUniqueClient called ---------------------------\n", randomLogId));
            try
            {
                Customer customer = new Customer();
                CustomerDbContext dbCustomerObj = new CustomerDbContext();
                customer = (dbCustomerObj.Connections.Single(m => m.CustomerId == customerId
                && m.IsActive == true));

                if (customer != null)
                {
                    if (customer.IsActive == true)
                    {
                        logger.Info(string.Format("SequenceId {0} : Customer details found and customer status is also active", randomLogId));
                    }
                }
                else
                {
                    logger.Info(string.Format("SequenceId {0} : Customer is inactive", randomLogId));
                }

                #region Dynamic URL of CRM Used 
                if (customer.CRMUsed == "Salesforce")
                {
                    //Get all the values for a specific client
                    SalesforceClient sfRef = new SalesforceClient();
                    sfRef.ClientId = customer.ConsumerKey;
                    sfRef.ClientSecret = customer.ConsumerSecretKey;
                    sfRef.Username = customer.UserName;

                    //Decrypt password
                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    byte[] todecode_byte = Convert.FromBase64String(customer.Password);
                    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    string decodedPassword = new String(decoded_char);
                    customer.Password = decodedPassword;

                    sfRef.Password = customer.Password;
                    sfRef.Token = customer.AuthToken;

                    //Below mentioned Lead we can Object we can get feed fot client specific in future
                    string instanceApiUrl = customer.InstanceName + _apiEndPoint;
                    //string strQuery = phoneNo + "&sobject=Lead&Lead.fields=id,name";
                    string strQuery = phoneNo + string.Format(ConnectMeResource.SalesForceStaticUrl, customer.APIEndPoint, customer.APIEndPoint);


                    sfRef.Login();
                    string authParam = string.Empty;

                    //Fetch the url for the specific lead
                    string queryOutput = sfRef.Query(instanceApiUrl, strQuery, out authParam);

                    //Deserialize the record
                    QueryResult QueryResultObj = JsonConvert.DeserializeObject<QueryResult>(queryOutput);

                    //Getting the correct url 18 char value to 15 char unique value
                    string strResponseURL = QueryResultObj.searchRecords[0].attributes.url;
                    int countSlash = strResponseURL.Length - strResponseURL.Replace("/", "").Length;
                    string[] urlParts = strResponseURL.Split('/');
                    string sfdcObjectId = urlParts[countSlash];
                    CRMAppendId = sfdcObjectId.Substring(0, sfdcObjectId.Length - 3);
                }
                else if (customer.CRMUsed == "Autotask")
                {
                    //CRMAppendId =  "Autotask/AutotaskExtend/ExecuteCommand.aspx?Code=OpenAccount&Phone=" + phoneNo;
                    CRMAppendId = ConnectMeResource.AutoTaskStaticUrl + phoneNo;
                }
                #endregion

                #region Calling  Specific Client
                BasicConfigurator.Configure();
                Connection connectionObj = new Connection();
                ConnectionDbContext dbObj = new ConnectionDbContext();

                //Fetching connection details of agent whome to push SF url
                connectionObj = (dbObj.Connections.Single(m => m.CustomerId == customerId
                && m.ExtensionNo == extensionNo && m.IsConnected == true));

                if (connectionObj != null)
                {
                    logger.Info(string.Format("SequenceId {0} : Agent found and is active", randomLogId));
                }
                else
                {
                    logger.Info(string.Format("SequenceId {0} : Agent not found or inactive", randomLogId));
                }

                //Hub proxy object
                var broadcast = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
                string agentInfo = customerId + "-" + extensionNo;

                //Fetching the CRM url to pass to specific client
                CRMUrl = customer.InstanceName + CRMAppendId;

                logger.Info(string.Format("SequenceId {0} : On Hub salesforce URL generated {1} and passed to Agent:{2}", randomLogId, CRMUrl, extensionNo));

                if (connectionObj != null)
                {
                    logger.Info("SendToUniqueClient() Agent - " + "-CustId:" + customerId + ",ExtensionNo:" + extensionNo + ",ConnectionId:" + connectionObj.AgentConnectionId);
                    RequestLogInDB(customerId, extensionNo, connectionObj.AgentConnectionId);
                    broadcast.Clients.Client(connectionObj.AgentConnectionId).PushToUniqueClient(agentInfo, CRMUrl, connectionObj.AgentConnectionId, customerId, extensionNo);

                    //To read RequestStatus table if false then error in opening SFInfo.
                    bool requestStatus = VerifyAgentRequestStatus(customerId, extensionNo, connectionObj.AgentConnectionId);

                    logger.Info(string.Format("SequenceId {0} : MyHub - SendToUniqueClient call successfully, but Agent aplication did not respond correctly. [ConnectionId:{1}, CustId:{2} ,ExtensionNo:{3}]", randomLogId, connectionObj.AgentConnectionId, customerId, extensionNo));
                }
                else
                {
                    logger.Info(string.Format("SequenceId {0} : MyHub - SendToUniqueClient call successfully, but no entry for the customer/agent pair in Database with. [ConnectionId:{1}, CustId:{2} ,ExtensionNo:{3}]", randomLogId, connectionObj.AgentConnectionId, customerId, extensionNo));
                }
                #endregion
            }
            catch (Exception ex)
            {
                logger.Error("-------------------------------------------------\n");
                logger.Error("Problem in SendToUniqueClient. Error: " + ex.Message);
                logger.Error("-------------------------------------------------\n");
            }
        }

        //Send SF info to all connected clients at once
        public void Send(string connectionId, string salesforceUrl)
        {
            try
            {
                BasicConfigurator.Configure();
                logger.Info("Send Method - broadcasting message to all connected clients.");
                var broadcast = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
                broadcast.Clients.All.addMessage("", salesforceUrl);
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in Send method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }
        }
        public override Task OnConnected()
        {
            try
            {
                //Called when Agents wants to connect to HUB.
                BasicConfigurator.Configure();
                string name = string.Empty;
                string customerId = string.Empty;
                string extensionNo = string.Empty;
                if (Context.QueryString["UserName"] != null)
                {
                    name = Context.QueryString["UserName"];
                    string clientConnectionId = Context.ConnectionId;
                    customerId = Context.QueryString["CustomerId"];
                    extensionNo = Context.QueryString["ExtensionNo"];
                    //Perform database operations.
                    PerformDatabaseOperations(name, clientConnectionId, customerId,
                        extensionNo);
                }
                logger.Info("OnConnected() -Agent:" + name + ",ConnectionId:" + Context.ConnectionId + ",CustomerId:" + customerId + ",ExtensionNo:" + extensionNo);
                return base.OnConnected();
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in OnConnected Method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
                return null;
            }
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                //Called to get the disconnected state of an agent.
                BasicConfigurator.Configure();
                Connection con = new Connection();

                string custId = Context.QueryString["CustomerId"];
                string extensionNo = Context.QueryString["ExtensionNo"];

                if ((custId != null) && (extensionNo != null))
                {
                    ConnectionDbContext dbObj = new ConnectionDbContext();

                    con = dbObj.Connections.Single(m => m.CustomerId == custId
                                                   && m.ExtensionNo == (extensionNo)
                                                   && m.IsConnected == true);
                    con.IsConnected = false;
                    dbObj.SaveChanges();
                    logger.Info("Agents disconnected Database updated- CustId:" + Context.QueryString["CustomerId"] + ",ExtensionNo:" + Context.QueryString["ExtensionNo"] + " ,ConnectionId:" + Context.ConnectionId);
                }
                logger.Info("OnDisconnected() Agent - ID-" + Context.ConnectionId + " ,CustId:" + custId + " ,Extension:" + extensionNo);
                return base.OnDisconnected(stopCalled);
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in OnDisConnected method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
                return null;
            }
        }
        public override Task OnReconnected()
        {
            try
            {
                //Called on reconnection of client
                BasicConfigurator.Configure();
                string name = Context.User.Identity.Name;

                logger.Info("OnReconnected() Agent:" + name + ",ConnectionId:" + Context.ConnectionId);
                return base.OnReconnected();
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in OnReConnected method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
                return null;
            }
        }

        #region Database Operations
        //To updated/insert into Connection table
        public void PerformDatabaseOperations(string Name, string ConnId,
            string CustomerId, string ExtensionNo)
        {
            try
            {
                BasicConfigurator.Configure();
                ConnectionDbContext dbObj = new ConnectionDbContext();

                Connection con = new Connection();
                //Check connection already exists or not
                con = (dbObj.Connections.FirstOrDefault(m => m.CustomerId == CustomerId
                && m.ExtensionNo == ExtensionNo));
                if (con != null)
                {
                    //Update database entry
                    con.AgentConnectionId = ConnId;
                    con.IsConnected = true;
                    con.ConnectionTime = DateTime.Now;

                    using (var context = new ConnectionDbContext())
                    {
                        var agentConnection = new Connection();
                        agentConnection = (dbObj.Connections.Single(m => m.CustomerId == CustomerId
                                             && m.ExtensionNo == ExtensionNo));

                        agentConnection.AgentConnectionId = ConnId;
                        agentConnection.ExtensionNo = ExtensionNo;
                        agentConnection.ConnectionTime = DateTime.Now;
                        agentConnection.IsConnected = true;

                        context.Connections.Attach(agentConnection);
                        context.SaveChanges();
                        context.SaveChanges();
                    }
                    dbObj.SaveChanges();
                    logger.Info("Agents connection information Updated in Database.-CustId:" + CustomerId + ",ExtensionNo:" + ExtensionNo);
                }
                else
                {
                    //Insert into database
                    using (var db = new ConnectionDbContext())
                    {
                        var agentConnection = new Connection
                        {
                            AgentConnectionId = ConnId,
                            IsConnected = true,
                            ConnectionTime = DateTime.Now,
                            CustomerId = CustomerId,
                            ExtensionNo = ExtensionNo
                        };
                        db.Connections.Add(agentConnection);
                        db.SaveChanges();
                        logger.Info("Agents connection information inserted in Database.-CustId:" + CustomerId + ",ExtensionNo:" + ExtensionNo);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in saving/updating records in database, please check configuration. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }
        }

        //Request Log storing in database from HubEnd and update from agents end to perfectly trace the API HIT
        private void RequestLogInDB(string CustomerId, string ExtensionNo,
            string ConnId)
        {
            try
            {
                RequestStatusDbContext reqDbObj = new RequestStatusDbContext();
                string requestTo = CustomerId.Trim() + "/" + ExtensionNo.Trim();
                RequestStatus reqStatObj = new RequestStatus();
                reqStatObj = (reqDbObj.ReqStatus.FirstOrDefault(m => m.RequestTo == requestTo));

                if (reqStatObj != null)
                {
                    //Update database entry
                    reqStatObj.AgentConnectionId = ConnId;
                    reqStatObj.IsSuccess = false;
                    reqStatObj.Datetime = DateTime.Now;

                    using (var reqSt = new RequestStatusDbContext())
                    {
                        var reqToAgent = new RequestStatus();
                        reqToAgent = (reqSt.ReqStatus.Single(m => m.RequestTo == requestTo));

                        reqToAgent.AgentConnectionId = ConnId;
                        reqToAgent.Datetime = DateTime.Now;
                        reqToAgent.IsSuccess = false;
                        reqSt.ReqStatus.Attach(reqToAgent);
                        reqSt.SaveChanges();
                    }
                    reqDbObj.SaveChanges();
                    logger.Info("Agents request information Updated in Database.-CustId:" + CustomerId + ",ExtensionNo:" + ExtensionNo + ",ConnectionId:" + ConnId);
                }
                else
                {
                    //Fresh request entry
                    using (var db = new RequestStatusDbContext())
                    {
                        var requestEntry = new RequestStatus
                        {
                            RequestFrom = "HUB",
                            RequestTo = requestTo,
                            AgentConnectionId = ConnId,
                            IsSuccess = false,
                            Datetime = DateTime.Now
                        };
                        db.ReqStatus.Add(requestEntry);
                        db.SaveChanges();
                        logger.Info("Request Status information inserted in Database.for-CustId:" + CustomerId + ",ExtensionNo:" + ExtensionNo + ",ConnectionId:" + ConnId);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in updating RequestLogInDB method. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
            }
        }
        //Required when reading that client updated the API hit request in database or not.
        private bool VerifyAgentRequestStatus(string customerId, string extensionNo, string agentConnectionId)
        {
            try
            {
                RequestStatusDbContext reqDbObj = new RequestStatusDbContext();
                string requestTo = customerId.Trim() + "/" + extensionNo.Trim();
                RequestStatus reqStatObj = new RequestStatus();
                reqStatObj = (reqDbObj.ReqStatus.FirstOrDefault(m => m.RequestTo == requestTo && m.IsSuccess == true));

                if (reqStatObj != null)
                    return reqStatObj.IsSuccess;
                else
                    return true;
            }
            catch (Exception ex)
            {
                logger.Error("-----------------------------------------------");
                logger.Error("Problem in VerifyAgentRequestStatus. Error: " + ex.Message);
                logger.Error("-----------------------------------------------");
                return false;
            }
        }

        //Request Log storing in database from Agents End 
        public bool ApiRequestUpdation(string requestTo)
        {
            try
            {
                BasicConfigurator.Configure();
                RequestStatusDbContext dbObj = new RequestStatusDbContext();
                RequestStatus reqStatus = new RequestStatus();

                //Check Request Entry already exists or not
                reqStatus = (dbObj.ReqStatus.FirstOrDefault(m => m.RequestTo == requestTo));

                if (reqStatus != null)
                {
                    reqStatus.IsSuccess = true;
                    reqStatus.Datetime = DateTime.Now;

                    using (var reqSt = new RequestStatusDbContext())
                    {
                        var reqToAgent = new RequestStatus();
                        reqToAgent = (reqSt.ReqStatus.Single(m => m.RequestTo == requestTo));

                        reqToAgent.Datetime = DateTime.Now;
                        reqToAgent.IsSuccess = true;
                        reqSt.ReqStatus.Attach(reqToAgent);
                        reqSt.SaveChanges();
                    }
                    dbObj.SaveChanges();
                    logger.Info("Agents Request Status Updated in Database-RequestTo:" + requestTo + ", ConnectionId:" + reqStatus.AgentConnectionId);
                    return true;
                }
                else
                {
                    logger.Info("Agents Request Status Not Updated in Database-RequestTo:" + requestTo + ", No entries found for mentioned RequestTo");
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("-------------------------------------------------\n");
                logger.Error("Problem in ApiRequestUpdation method from Agent's end. Error: " + ex.Message);
                logger.Error("-------------------------------------------------\n");
                return false;
            }
        }
    }
    #endregion

}