using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AlfavoxClient
{
    public static class Service
    {
        private static string _serviceUrl;
        public static string ServiceUrl => _serviceUrl ?? (_serviceUrl = System.Configuration.ConfigurationManager.AppSettings["ServiceUrl"]);

        public static async Task<IDictionary<int, string>> GetDataAsync(string model)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, ServiceUrl)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
                    };

                    var response = client.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IDictionary<int, string>>(responseBody);
                }
                catch
                {
                    throw;
                }
            }
        }

        public static async Task<IDictionary<int, string>> GetDataAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync($"{ServiceUrl}/{id}").Result;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return new Dictionary<int, string> { { id, JsonConvert.DeserializeObject<string>(responseBody) ?? "Brak wartości" } };
                }
                catch
                {
                    throw;
                }
            }
        }

        // alternative 'post';
        public static IDictionary<int, string> GetData(string model)
        {
            WebClient proxy = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8,
            };
            proxy.Headers.Add("Content-Type", "application/json");
            return JsonConvert.DeserializeObject<IDictionary<int, string>>(proxy.UploadString(ServiceUrl, "POST", JsonConvert.SerializeObject(model)));
        }
    }
}