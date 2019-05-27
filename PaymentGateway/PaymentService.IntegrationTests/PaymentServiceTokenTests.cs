using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestsHelper;
using Xunit;

namespace PaymentService.IntegrationTests
{
    public class PaymentServiceTokenTests
    {
        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerStatusUnAuthorized()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.Unauthorized;
           
            //Act
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Remove("Authorization");

                var response = await httpClient.GetAsync(Configuration.PaymentsUri);

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
                httpClient.DefaultRequestHeaders.Add("Authorization", Configuration.FakeToken);

                var response = await httpClient.GetAsync($"{Configuration.PaymentsUri}/{paymentId}");

                // Assert
                response.StatusCode.ShouldBe(expectedStatusCode);
            }
        }
    }
}