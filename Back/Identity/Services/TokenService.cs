using IdentityModel.Client;
using IdentityServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityServer.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;
        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GenerateTokenAsync(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourLongSecretKeyHere1234567890123456"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "Jwt: Issuesr",
                audience: "Jwt: Audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var seurityToken =  new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(seurityToken);
        }

        public async Task<string> GetTokenAsync()
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync("http://localhost:5001");

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            { 
                Address = disco.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "api1"
            });
            
            if (tokenResponse.IsError) 
            { 
                throw new Exception(tokenResponse.Error);
            }

            return tokenResponse.AccessToken;
        }
    }
}
