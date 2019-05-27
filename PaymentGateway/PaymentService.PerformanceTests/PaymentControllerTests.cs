﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace PaymentService.PerformanceTests
{
    public class PaymentControllerTests
    {
        private readonly string _fakeToken = "393baf4d-13b6-4905-a9d3-33cf2e5560f4";
        private readonly int expectedRequestTime = 500;
        private readonly int expectedAvarageRequestTime = 500;

        [Fact]
        public void RequestTime()
        {
            //Arrange
            var paymentId = new Guid();
            var requestUri = string.Format("{0}/{1}", "https://localhost:44308/api/payments", paymentId);

            //Act
            DateTime start;
            DateTime end;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _fakeToken);

                start = DateTime.Now;
                var response = client.GetAsync(requestUri).Result;
                end = DateTime.Now;
            }
            var actual = (int)(end - start).TotalMilliseconds;

            //Assert
            Assert.True(actual <= expectedRequestTime,
                $"Expected total milliseconds of less than or equal to {expectedRequestTime} but was {actual}.");
        }

        [Fact]
        public void AverageResponseTime_100Requests()
        {
            //Arrange
            var paymentId = new Guid();
            var requestUri = string.Format("{0}/{1}", "https://localhost:44308/api/payments", paymentId);
            var allResponseTimes = new List<(DateTime Start, DateTime End)>();

            //Act
            for (var i = 0; i < 100; i++)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", _fakeToken);
                    var start = DateTime.Now;
                    var response = client.GetAsync(requestUri).Result;
                    var end = DateTime.Now;

                    allResponseTimes.Add((start, end));
                }
            }

            var actual = (int)allResponseTimes.Select(r => (r.End - r.Start).TotalMilliseconds).Average();

            //Assert
            Assert.True(actual <= expectedAvarageRequestTime,
                $"Expected average response time of less than or equal to {expectedAvarageRequestTime} ms but was {actual} ms.");
        }
    }
}