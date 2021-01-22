using Assets.Common;
using Assets.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Web
{
    public class HttpServiceClient : IHttpServiceClient
    {
        public const string FridgeNotesUri = "http://fridgenotes.azurewebsites.net";
        public const string ApplicationJsonContentType = "application/json";

        public async Task<T> DeleteAsync<T>(string endpoint, object content = null)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ApplicationJsonContentType);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{FridgeNotesUri}/{endpoint}"),
                Method = HttpMethod.Delete,
                Content = payload
            };

            return await SendAsync<T>(request);
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{FridgeNotesUri}/{endpoint}"),
                Method = HttpMethod.Get,
            };

            return await SendAsync<T>(request);
        }

        public async Task<T> PostAsync<T>(string endpoint, object content)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ApplicationJsonContentType);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{FridgeNotesUri}/{endpoint}"),
                Method = HttpMethod.Post,
                Content = payload
            };

            return await SendAsync<T>(request);
        }

        public async Task<T> PutAsync<T>(string endpoint, object content)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ApplicationJsonContentType);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{FridgeNotesUri}/{endpoint}"),
                Method = HttpMethod.Put,
                Content = payload
            };

            return await SendAsync<T>(request);
        }

        public async Task PutAsync(string endpoint, object content)
        {
            var payload = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ApplicationJsonContentType);

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{FridgeNotesUri}/{endpoint}"),
                Method = HttpMethod.Put,
                Content = payload
            };

            await SendAsync(request);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage request)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", StateManager.CurrentAppState?.Token);

                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new HttpResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());

                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseBody);
                }
            }
        }

        private async Task SendAsync(HttpRequestMessage request)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", StateManager.CurrentAppState?.Token);

                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new HttpResponseException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
