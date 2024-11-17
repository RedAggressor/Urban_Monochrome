using Nitifacation.Host.Models;
using Nitifacation.Host.Services.Interfaces;
using System.Text;

namespace Nitifacation.Host.Services
{
    public class SendPulseService : ISendPulseService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly IHttpClientService _httpClientService;
        private readonly string _pulseUrlToken;

        public SendPulseService(IConfiguration configuration, IHttpClientService httpClientService)
        {
            _clientId = configuration["SendPuls:ClientId"]!;
            _clientSecret = configuration["SendPuls:ClientSecret"]!;
            _httpClientService = httpClientService;
            _pulseUrlToken = configuration["SendPuls:UrlToken"]!;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var content = new RequestBodyAccess
            { 
                ClientId = _clientId,
                ClientSecret = _clientSecret                
            };

            var responseToken = await _httpClientService.SendAsync<ResponseTokenPulse, RequestBodyAccess> (_pulseUrlToken, HttpMethod.Post, content);

            return responseToken.AccessToken;
        }

        public async Task SendMailAsync(string to, string subject, string body)
        {
            var accessToken = await GetAccessTokenAsync();
            using (var client = new HttpClient()) 
            { 
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var emailData = new 
                { 
                    email = new 
                    { 
                        subject = subject,
                        from = new 
                        { 
                            name = "Your Name",
                            email = "cadacRedl@outlook.com"
                        },
                        text = "test body",
                        html = Convert.ToBase64String(Encoding.UTF8.GetBytes(body))
                    },
                    to = new[]
                        {
                            new
                            {
                                name = "Max",
                                email = to
                            }
                        },

                };
                
                var response = await client.PostAsJsonAsync("https://api.sendpulse.com/smtp/emails", emailData);
                response.EnsureSuccessStatusCode();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to send email: {response.StatusCode}, {errorContent}");
                }
            }
        }
    }
}
