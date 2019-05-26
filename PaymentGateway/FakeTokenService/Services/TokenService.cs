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
            if (string.IsNullOrEmpty(tokenId))
                return false;

            if (tokenId.Equals(_configuration["FakeToken"]))
                return true;

            return false;
        }
    }
}