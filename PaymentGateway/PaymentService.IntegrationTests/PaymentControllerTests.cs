using PaymentService.Core.ReponseModels;
using PaymentService.Core.RequestModels;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PaymentService.IntegrationTests
{
    public class PaymentControllerTests
    {
        private readonly string _fakeToken = "393baf4d-13b6-4905-a9d3-33cf2e5560f4";
        private readonly string paymentsUri = "https://localhost:44308/api/payments";

        [Fact]
        public async Task WhenGetMethodIsInvokedWithAnEmptyPaymentId_GetShouldAnswerStatusNotFound()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.NotFound;
            var paymentId = new Guid();
            var requestUri = string.Format("{0}/{1}", paymentsUri, paymentId);

            //Act
            using (var _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", _fakeToken);

                var response = await _httpClient.GetAsync(requestUri);

                // Assert
                response.StatusCode.ShouldBe(expectedStatusCode);
            }
        }

        [Fact]
        public async Task WhenPostMethodIsInvokedWithValidPayment_PostShouldAnswerStatusOk()
        {
            //Arrange
            var payment = new PaymentProcessRequest
            {
                CardNumber = "1234567812345678",
                Ccv = "077",
                ExpiryYear = 2021,
                ExpiryMonth = 11,
                Currency = "EUR",
                Amount = (decimal)2.25
            };

            //Act
            using (var _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", _fakeToken);

                var response = await _httpClient.PostAsJsonAsync(paymentsUri, payment);

                // Assert
                response.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task WhenPostMethodIsInvokedWithValidPayment_GetShouldAnswerStatusOk()
        {
            //Arrange
            var payment = new PaymentProcessRequest
            {
                CardNumber = "1234567812345678",
                Ccv = "077",
                ExpiryYear = 2021,
                ExpiryMonth = 11,
                Currency = "EUR",
                Amount = (decimal)2.25
            };

            //Act
            using (var _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", _fakeToken);

                var response = await _httpClient.PostAsJsonAsync(paymentsUri, payment);
                Guid createdPaymentId = new Guid();

                if (response.IsSuccessStatusCode)
                {
                    var paymentCreated = await response.Content.ReadAsAsync<PaymentProcessResponse>();
                    createdPaymentId = paymentCreated.PaymentId;
                }

                var requestUri = string.Format("{0}/{1}", paymentsUri, createdPaymentId.ToString());
                var getPaymentResponse = await _httpClient.GetAsync(requestUri);

                // Assert
                getPaymentResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }
    }
}