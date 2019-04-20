using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using NLog;

namespace RandomUser
{
    public class UserAgent : HttpClient
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        public UserAgent()
        {
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.BaseAddress = new Uri(@"https://randomuser.me");
            this.DefaultRequestHeaders.Add("X-CSRF-Header", String.Empty);
        }

        protected JObject GetRandomUserObject()
        {
            var response = this.GetJsonResponse("api");
            var results = response["results"] as JArray;
            return results.Single() as JObject;
        }

        protected JArray GetRandomUserObjects(int userCount, string gender)
        {
            string url = $"api/?results={userCount}&gender={gender}";
            var response = this.GetJsonResponse(url);
            var results = response["results"] as JArray;
            return results;
        }

        private JObject GetJsonResponse(string url)
        {
            JObject result = null;

            HttpResponseMessage response = this.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                result = JObject.Parse(json);
            }
            else
            {
                logger.Warn($"The results failed with the status code {response.StatusCode}.");
            }

            return result;
        }
    }
}
