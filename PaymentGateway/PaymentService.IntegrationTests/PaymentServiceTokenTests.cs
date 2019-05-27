using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PaymentService.IntegrationTests
{
    public class PaymentServiceTokenTests
    {
        private const string FakeToken = "393baf4d-13b6-4905-a9d3-33cf2e5560f4";

        private const string PaymentsUri = "https://localhost:44308/api/payments";

        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerStatusUnAuthorized()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.Unauthorized;
           
            //Act
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Remove("Authorization");

                var response = await httpClient.GetAsync(PaymentsUri);

                // Assert
                response.StatusCode.ShouldBe(expectedStatusCode);
            }
        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithAValidToken_GetShouldAnswerWithStatusNotFound()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.NotFound;
            var paymentId = new Guid();

            //Act
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", FakeToken);

                var response = await httpClient.GetAsync($"{PaymentsUri}/{paymentId}");

                // Assert
                response.StatusCode.ShouldBe(expectedStatusCode);
            }
        }
    }
}