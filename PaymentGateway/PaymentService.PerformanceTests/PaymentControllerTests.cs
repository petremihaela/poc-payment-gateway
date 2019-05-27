using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TestsHelper;
using Xunit;

namespace PaymentService.PerformanceTests
{
    public class PaymentControllerTests
    {
        private const int ExpectedRequestTime = 500;
        private const int ExpectedAverageRequestTime = 500;

        [Fact]
        public async System.Threading.Tasks.Task RequestTimeAsync()
        {
            //Arrange
            var paymentId = new Guid();
            var requestUri = $"{Configuration.PaymentsUri}/{paymentId}";

            //Act
            DateTime start;
            DateTime end;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", Configuration.FakeToken);

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
            var requestUri = $"{Configuration.PaymentsUri}/{paymentId}";
            var allResponseTimes = new List<(DateTime Start, DateTime End)>();

            //Act
            for (var i = 0; i < 100; i++)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", Configuration.FakeToken);
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