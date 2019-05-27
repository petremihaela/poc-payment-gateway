using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace PaymentService.PerformanceTests
{
    public class PaymentControllerTests
    {
        public readonly string FakeToken = "393baf4d-13b6-4905-a9d3-33cf2e5560f4";
        private const int ExpectedRequestTime = 500;
        private const int ExpectedAverageRequestTime = 500;

        private const string PaymentsUri = "https://localhost:44308/api/payments";

        [Fact]
        public async System.Threading.Tasks.Task RequestTimeAsync()
        {
            //Arrange
            var paymentId = new Guid();
            var requestUri = $"{PaymentsUri}/{paymentId}";

            //Act
            DateTime start;
            DateTime end;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", FakeToken);

                start = DateTime.Now;
                await client.GetAsync(requestUri);
                end = DateTime.Now;
            }
            var actual = (int)(end - start).TotalMilliseconds;

            //Assert
            Assert.True(actual <= ExpectedRequestTime,
                $"Expected total milliseconds of less than or equal to {ExpectedRequestTime} but was {actual}.");
        }

        [Fact]
        public async System.Threading.Tasks.Task AverageResponseTime_100RequestsAsync()
        {
            //Arrange
            var paymentId = new Guid();
            var requestUri = $"{PaymentsUri}/{paymentId}";
            var allResponseTimes = new List<(DateTime Start, DateTime End)>();

            //Act
            for (var i = 0; i < 100; i++)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", FakeToken);
                    var start = DateTime.Now;
                    await client.GetAsync(requestUri);
                    var end = DateTime.Now;

                    allResponseTimes.Add((start, end));
                }
            }

            var actual = (int)allResponseTimes.Select(r => (r.End - r.Start).TotalMilliseconds).Average();

            //Assert
            Assert.True(actual <= ExpectedAverageRequestTime,
                $"Expected average response time of less than or equal to {ExpectedAverageRequestTime} ms but was {actual} ms.");
        }
    }
}