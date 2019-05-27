using Microsoft.Extensions.Logging;
using PaymentService.Core.ReponseModels;
using PaymentService.Core.RequestModels;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Managers.PaymentProcessor
{
    public class FakePaymentProcessor : IPaymentProcessor
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly ILogger<FakePaymentProcessor> _logger;

        public FakePaymentProcessor(IHttpClientFactory clientFactory, ILogger<FakePaymentProcessor> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<PaymentProcessResponse> ProcessPaymentAsync(PaymentProcessRequest payment)
        {
            try
            {
                var resource = $"api/payment/";
                var content = new StringContent(resource.ToString(), Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient("paymentsProcessor");
                var response = await client.PostAsync(resource, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<PaymentProcessResponse>();
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