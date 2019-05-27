using Microsoft.Extensions.Logging;
using PaymentService.Core.RequestModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using PaymentService.Core.ResponseModels;

namespace PaymentService.Managers.PaymentProcessor
{
    public class FakePaymentProcessor : IPaymentProcessor
    {
        private readonly ILogger<FakePaymentProcessor> _logger;

        private readonly IHttpClientFactory _clientFactory;

        public FakePaymentProcessor(ILogger<FakePaymentProcessor> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<PaymentProcessResponse> ProcessPaymentAsync(PaymentProcessRequest payment)
        {
            try
            {
                var requestUri = "api/payments";
                var client = _clientFactory.CreateClient("paymentsProcessor");

                var response = await client.PostAsJsonAsync(requestUri, payment);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PaymentProcessResponse>();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
    }
}