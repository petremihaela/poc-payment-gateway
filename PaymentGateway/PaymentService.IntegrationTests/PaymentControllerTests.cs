using PaymentService.Core.RequestModels;
using PaymentService.Core.ResponseModels;
using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestsHelper;
using Xunit;

namespace PaymentService.IntegrationTests
{
    public class PaymentControllerTests
    {
        [Fact]
        public async Task WhenGetMethodIsInvokedWithAnEmptyPaymentId_GetShouldAnswerStatusNotFound()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.NotFound;
            var paymentId = new Guid();
            var requestUri = $"{Configuration.PaymentsUri}/{paymentId}";

            //Act
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", Configuration.FakeToken);

                var response = await httpClient.GetAsync(requestUri);

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
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", Configuration.FakeToken);

                var response = await httpClient.PostAsJsonAsync(Configuration.PaymentsUri, payment);

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
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", Configuration.FakeToken);

                var response = await httpClient.PostAsJsonAsync(Configuration.PaymentsUri, payment);
                var createdPaymentId = new Guid();

                if (response.IsSuccessStatusCode)
                {
                    var paymentCreated = await response.Content.ReadAsAsync<PaymentProcessResponse>();
                    createdPaymentId = paymentCreated.PaymentId;
                }

                var requestUri = $"{Configuration.PaymentsUri}/{createdPaymentId.ToString()}";
                var getPaymentResponse = await httpClient.GetAsync(requestUri);

                // Assert
                getPaymentResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
            }
        }
    }
}