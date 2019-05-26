using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentService.Managers.Token
{
    public class TokenManager : ITokenManager
    {
        private readonly IHttpClientFactory _clientFactory;

        public TokenManager(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            bool result = false;
            try
            {
                var path = $"api/tokens/{token}";

                var request = new HttpRequestMessage(HttpMethod.Get, path);

                var client = _clientFactory.CreateClient("tokensProvider");

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<bool>();
                }
            }
            catch (Exception)
            {
                //Logging
            }

            return result;
        }
    }
}