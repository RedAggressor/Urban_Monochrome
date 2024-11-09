using Infrastucture.Services.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace Infrastucture.Services
{
    public class HttpClientService : IHttpClientService
    {
        public readonly IHttpClientFactory _httpClient;

        public HttpClientService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
        {
            var client = _httpClient.CreateClient();
            var httpMessage = new HttpRequestMessage();
            httpMessage.RequestUri = new Uri(url);
            httpMessage.Method = method;

            if(content is not null)
            {
                httpMessage.Content = new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json");
            }

            var result = await client.SendAsync(httpMessage);

            if(result.IsSuccessStatusCode)
            {
                var resultContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(resultContent);
                return response!;
            }

            return default(TResponse)!;
        }
    }
}
