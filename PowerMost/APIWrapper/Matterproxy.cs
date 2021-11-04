using System;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using RestSharp.Authenticators;
using PowerMost.Model;

namespace PowerMost.APIWrapper
{
    class Matterproxy
    {

        private readonly string username;
        private readonly string password;
        private readonly string testurl = "http://127.0.0.1:8065/api/v4";

        const string testusername = "testuser";
        const string testpassword = "testpassword";

        private string accesstoken;
        private string baseurl; 
        private string sessiontoken; 
        private bool useSessionToken = true; 

        public Matterproxy()
        {
            this.baseurl = testurl;
            this.username = testusername;
            this.password = testpassword; 
        }

        public Matterproxy(string baseurl, string username, string password)
        {
            this.baseurl = baseurl;
            this.username = username;
            this.password = password; 

        }

        public Matterproxy(string baseurl, string accesstoken)
        {
            this.baseurl = baseurl;
            this.username = string.Empty; 
            this.accesstoken = accesstoken;
            useSessionToken = false; 
        }

        private bool IsAuthenticated()
        {
            var client = new RestClient(baseurl);
            var req = new RestRequest("users/me", Method.GET, DataFormat.Json);

            var token = useSessionToken ? sessiontoken : accesstoken;
            req.AddHeader("Authorization", string.Format("Bearer {0}", token));

            var res = client.Execute<MMUser>(req);

            if(res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return false; 
            }

            if (!String.IsNullOrEmpty(username) && !res.Data.username.Equals(username, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true; 

        }

        private void AuthenticateSession(string username, string password)
        {
            var client = new RestClient(baseurl);
            var param = new { login_id = username, password = password };
            var request = new RestRequest("users/login", Method.POST, DataFormat.Json);
            request.AddJsonBody(param);
            var response = client.Post(request);

            foreach(Parameter p in response.Headers)
            {
                if(p.Name.Equals("Token"))
                {
                    sessiontoken = p.Value.ToString();
                    break; 
                }
            }

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(string.Format("Authentication Error: {0}", response.ErrorMessage)); 
            }

        }


        public T Execute<T>(RestRequest request) where T : new()
        {
            if(!IsAuthenticated()) { AuthenticateSession(username, password); }

            var client = new RestClient(baseurl);

            var token = useSessionToken ? sessiontoken : accesstoken; 
            request.AddHeader("Authorization", string.Format("Bearer {0}", token)); 

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                throw new Exception(message, response.ErrorException);
            }

            return response.Data;

        }

        public void TestConnection()
        {

            var request = new RestRequest("users/me");
            Console.WriteLine(Execute<MMUser>(request).first_name); 
  

        }

    }
}
