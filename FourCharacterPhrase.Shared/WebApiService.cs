using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FourCharacterPhrase.Shared
{
    public static class WebApiService
    {
        private static HttpClient Http = new HttpClient();
        private const string baseURL = "https://fourcharacterphraseserver.azurewebsites.net/";
        //private const string baseURL = "http://localhost:50952/";
        //private const string baseURL = "http://localhost:60111/";
        public static async Task<object> PostRequest<T>(string serviceName, T sendObject)
        {
            string jsonString = JsonConvert.SerializeObject(sendObject);
            var requestUri = baseURL + serviceName;
            Console.WriteLine($"post:{requestUri}");
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(requestUri),
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await Http.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode(); //will throw an exception if not successful

            var responseContent = await response.Content.ReadAsStringAsync();
            return "";
            //return JObject.Parse(responseContent);
        }

        public static async Task<T> GetRequest<T,U>(string serviceName)
        {
            var requestUri = $"{baseURL}{serviceName}";
            Console.WriteLine($"get:{requestUri}");
            var response = await Http.GetAsync(requestUri);
            response.EnsureSuccessStatusCode(); //will throw an exception if not successful
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public static async Task<object> DeleteRequest<T>(string serviceName, T sendObject)
        {
            string jsonString = JsonConvert.SerializeObject(sendObject);
            var requestUri = baseURL + serviceName;
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod("DELETE"),
                RequestUri = new Uri(requestUri),
                Content = new StringContent(jsonString, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await Http.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode(); //will throw an exception if not successful

            var responseContent = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseContent);
        }
    }
}
