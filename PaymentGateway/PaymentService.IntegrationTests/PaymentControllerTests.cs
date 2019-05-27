using Shouldly;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PaymentService.IntegrationTests
{
    public class PaymentControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _httpClient;

        private readonly string _fakeToken = "393baf4d-13b6-4905-a9d3-33cf2e5560f4";

        public PaymentControllerTests(TestFixture<Startup> fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithAnemptyPaymentId_GetShouldAnswerStatusNotFound()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.NotFound;
            _httpClient.DefaultRequestHeaders.Add("Authorization",_fakeToken);
            var paymentId = new Guid();

            // Act
            var response = await _httpClient.GetAsync($"/api/payments/{paymentId}");

            // Assert
            response.StatusCode.ShouldBe(expectedStatusCode);
        }
    }
}