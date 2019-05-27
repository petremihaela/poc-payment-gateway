using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentService.Managers.Token
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<TokenManager> _logger;

        public TokenManager(IHttpClientFactory clientFactory, ILogger<TokenManager> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            bool result = false;
            try
            {
                var resource = $"api/tokens/{token}";

                var request = new HttpRequestMessage(HttpMethod.Get, resource);

                var client = _clientFactory.CreateClient("tokensProvider");
                
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            return result;
        }
    }
}