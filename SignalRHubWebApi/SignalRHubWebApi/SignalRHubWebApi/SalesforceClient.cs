using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SignalRHubWebApi
{
    public class SalesforceClient
    {
        #region private variables
        private static readonly ILog logger = LogManager.GetLogger(typeof(SalesforceClient));

        private const string LOGIN_ENDPOINT = "https://login.salesforce.com/services/oauth2/token";
        private const string API_ENDPOINT = "/services/data/v36.0/";

        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthToken { get; set; }
        public string InstanceUrl { get; set; }
        #endregion
        static SalesforceClient()
        {
            // SF requires TLS 1.1 or 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }

        #region methods

        public void Login()
        {
            try
            {
                String jsonResponse;
                using (var client = new HttpClient())
                {
                    var request = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "password"},
                    {"client_id", ClientId},
                    {"client_secret", ClientSecret},
                    {"username", Username},
                    {"password", Password + Token}
                }
                    );
                    //JsonRespons shows the data or error types in case of failure.
                    request.Headers.Add("X-PrettyPrint", "1");
                    var response = client.PostAsync(LOGIN_ENDPOINT, request).Result;
                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                }
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                AuthToken = values["access_token"];
                InstanceUrl = values["instance_url"];
            }
            catch(Exception ex)
            {
                logger.Error("-------------------------------------------------\n");
                logger.Error("Login method of salesforce gives error. Error: " + ex.Message);
                logger.Error("-------------------------------------------------\n");
            }
        }

        public string Query(string soqlInstanceApiUrl,string soqlQuery, out string authToken)
        {
            using (var client = new HttpClient())
            {
                string restRequest = soqlInstanceApiUrl + "?q=" + soqlQuery;
                var request = new HttpRequestMessage(HttpMethod.Get, restRequest);
                request.Headers.Add("Authorization", "Bearer " + AuthToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("X-PrettyPrint", "1");
                var response = client.SendAsync(request).Result;
                authToken = AuthToken;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        #endregion
    }
}