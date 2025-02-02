using IdentityModel.Client;
using Infrastucture.Configuration;
using Infrastucture.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Infrastucture.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public HttpClientService(
            IHttpClientFactory httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<TResponse> SendAsync<TResponse, TRequest>(string url, HttpMethod method, TRequest? content)
        {
            var client = _httpClient.CreateClient();


            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                 Address = $"{_configuration["Authorization:Authority"]}/connect/token",

                 ClientId = _configuration["Client:Id"]!,
                 ClientSecret = _configuration["Client:Secret"]                 
            });

            client.SetBearerToken(tokenResponse.AccessToken!);

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
