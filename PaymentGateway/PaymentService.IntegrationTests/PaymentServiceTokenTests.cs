using Shouldly;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PaymentService.IntegrationTests
{
    public class PaymentServiceTokenTests : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _httpClient;

        private readonly string _fakeToken = "393baf4d-13b6-4905-a9d3-33cf2e5560f4";

        public PaymentServiceTokenTests(TestFixture<Startup> fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithoutAValidToken_GetShouldAnswerStatusUnAuthorized()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.Unauthorized;
            _httpClient.DefaultRequestHeaders.Remove("Authorization");

            // Act
            var response = await _httpClient.GetAsync("/api/payments/");

            // Assert
            response.StatusCode.ShouldBe(expectedStatusCode);
        }

        [Fact]
        public async Task WhenGetMethodIsInvokedWithAValidToken_GetShouldAnswerStatusOk()
        {
            //Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            _httpClient.DefaultRequestHeaders.Add("Authorization", _fakeToken);

            // Act
            var response = await _httpClient.GetAsync("/api/payments/");

            // Assert
            response.StatusCode.ShouldBe(expectedStatusCode);
        }
    }
}