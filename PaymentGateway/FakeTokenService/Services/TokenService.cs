using Microsoft.Extensions.Configuration;

namespace FakeTokenService.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidToken(string tokenId)
        {
            return !string.IsNullOrEmpty(tokenId) && tokenId.Equals(_configuration["FakeToken"]);
        }
    }
}