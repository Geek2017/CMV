using log4net;
using log4net.Config;
using Microsoft.AspNet.SignalR.Client;
using MVCAgent;
using MVCAgent.Resources;
using MVCAgentApplication.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MVCAgentApplication
{
    public class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
        public static string AgentIdentity = string.Empty;
        public static bool agentNameExists = false;
        public static bool extensionNoExists = false;
        public static bool customerIdExists = false;
        static dynamic connectionProxy = "";
        private static Mutex mutex = null;

        static void Main(string[] args)
        {
            try
            {
                //Only one instance of application should run.
                const string appName = "MVCAgent";
                bool createdNew;

                mutex = new Mutex(true, appName, out createdNew);
                if (!createdNew)
                {
                    logger.Info(ConnectMeResource.OneInstanceAlreadyRunning);
                    //App is already running! Exiting the application  
                    return;
                }

                // If AgentInfo folder not exists then create in the path
                string strAgentInfo = Application.StartupPath + "\\Documents"; //System.Configuration.ConfigurationSettings.AppSettings.Get("AgentInfopath").ToString();
                Directory.CreateDirectory(strAgentInfo);

                CheckRecordExistsOrNot();

                // To start the instance of winform
                Application.EnableVisualStyles();
                Application.Run(new AgentLoginForm());
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerMainMethodError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        //Currently not using its output, Essential future use.
        private static void CheckRecordExistsOrNot()
        {
            try
            {
                string strAgentInfo = ConfigurationManager.AppSettings.Get("AgentInfo").ToString();

                var fileLines = File.ReadAllLines(strAgentInfo);
                List<string> fileItems = new List<string>(fileLines);

                agentNameExists = fileItems.Contains("AgentName");
                extensionNoExists = fileItems.Contains(EnumAttributes.Attribute1);
                customerIdExists = fileItems.Contains(EnumAttributes.Attribute2);

            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerCheckRecordExistsOrNotMethodError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        public static void AgentExitLog()
        {
            try
            {
                //To make log of agents exit info.
                var connectionProxyToDisconnect = (Connection)connectionProxy;
                connectionProxyToDisconnect.Stop();
                logger.Info(AgentIdentity + " Exit.");
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerAgentCannotExitProperlyError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        public static void AgentLoggedOut()
        {
            try
            {
                //To make log of agents logout info.
                var connectionProxyLoggedOut = (Connection)connectionProxy;
                connectionProxyLoggedOut.Stop();
                logger.Info(AgentIdentity + ConnectMeResource.AgentLoggedOut);
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerAgentFacedErrorDuringLoggingOutError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        public static void ConnecteToHub(string customerId, string extensionNo, string agentName)
        {
            try
            {
                BasicConfigurator.Configure();

                string strAgentInfo = System.Configuration.ConfigurationSettings.AppSettings.Get("AgentInfo").ToString();

                #region Creating SpecificAgent at Agent start time
                try
                {
                    Agent agentInfo = new Agent
                    {
                        AgentName = agentName,
                        ExtensionNo = extensionNo,
                        CustomerId = customerId
                    };
                    using (StreamWriter file = File.CreateText(strAgentInfo))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        //Serialize object directly into file stream
                        serializer.Serialize(file, agentInfo);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ConnectMeResource.SeperationLine);
                    logger.Error(ConnectMeResource.LoggerProblemAtCreatingSpecificAgentError + ex.Message);
                    logger.Error(ConnectMeResource.SeperationLine);
                }

                #endregion

                #region Reading connected Agentname
                try
                {
                    using (StreamReader r = new StreamReader(strAgentInfo))
                    {
                        string json = r.ReadToEnd();
                        Agent items = JsonConvert.DeserializeObject<Agent>(json);
                        agentName = items.AgentName;
                        customerId = items.CustomerId;
                        extensionNo = items.ExtensionNo;
                    }
                    AgentIdentity = agentName;
                }
                catch (Exception ex)
                {
                    logger.Error(ConnectMeResource.SeperationLine);
                    logger.Error(ConnectMeResource.LoggerErrorAtReadingAgentsCredentialsFromConfigFileError + ex.Message);
                    logger.Error(ConnectMeResource.SeperationLine);
                }

                #endregion

                string strKey = System.Configuration.ConfigurationSettings.AppSettings.Get("HubConnection").ToString();
                var connection = new HubConnection(strKey, new Dictionary<string, string>
                {
                    { "UserName", agentName},
                    { "CustomerId", customerId},
                    { "ExtensionNo", extensionNo}
                });

                var myHub = connection.CreateHubProxy(EnumAttributes.Attribute3);

                connection.Start().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Global.GlobalVar = ConnectMeResource.ThereWasAnErrorOpeningTheConnection;
                        Global.GlobalStatus = false;
                        log4net.Config.BasicConfigurator.Configure();
                        ILog log = log4net.LogManager.GetLogger(typeof(Program));
                        log.Error(ConnectMeResource.LoggerConnectionTaskIsFaultedError + Global.GlobalVar);
                    }
                    else
                    {
                        logger.Info(ConnectMeResource.InfoSuccessfullyConnectedAgent + agentName + ConnectMeResource.InfoCustomerId + customerId + ConnectMeResource.InfoExtensionNo + extensionNo);
                        Global.GlobalVar = ConnectMeResource.SuccessfullyConnected + agentName + ConnectMeResource.WithServer;
                        Global.GlobalStatus = true;
                        try
                        {
                            myHub.On<string, string, string, string, string>("PushToUniqueClient", (AgentName, UrlToOpen, sConnectionId, sCustomerId, sExtensionNo) =>
                            {
                                #region To update DB for log
                                string requestTo = sCustomerId.Trim() + "/" + sExtensionNo.Trim();
                                System.Diagnostics.Process.Start(UrlToOpen);
                                logger.Info(ConnectMeResource.For + AgentName + ConnectMeResource.SuccessfullyOpenedUrl + UrlToOpen);

                                logger.Info(ConnectMeResource.InvokedHubMethodApiRequestUpdation + requestTo + ConnectMeResource.CloseBracket);
                                myHub.Invoke("ApiRequestUpdation", requestTo).ConfigureAwait(true);
                                #endregion
                            });
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ConnectMeResource.SeperationLine);
                            logger.Error(ConnectMeResource.LoggerProblemAtPushToUniqueClientError + ex.Message);
                            logger.Error(ConnectMeResource.SeperationLine);
                        }
                    }
                }).Wait();

                connectionProxy = connection;
                connection.StateChanged += Connection_StateChanged;
            }
            catch (Exception ex)
            {
                logger.Error(ConnectMeResource.SeperationLine);
                logger.Error(ConnectMeResource.LoggerErrorAtConnectToHubError + ex.Message);
                logger.Error(ConnectMeResource.SeperationLine);
            }
        }

        //Invoked when Hub is down by iis or by hardware.
        private static void Connection_StateChanged(StateChange conObj)
        {
            if (conObj.NewState == ConnectionState.Disconnected)
            {
                var connectionProxyHubStopped = (Connection)connectionProxy;
                connectionProxyHubStopped.Stop();

                logger.Info(ConnectMeResource.LoggedAgentIsDisconnected);
                MessageBox.Show(ConnectMeResource.LoggedAgentIsDisconnected);

                AgentLoggedOut();
            }
        }
    }
}
