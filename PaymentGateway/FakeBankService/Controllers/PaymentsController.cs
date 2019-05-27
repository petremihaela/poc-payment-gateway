using FakeBankService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FakeBankService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult<PaymentProcessResponse> ProcessPayment([FromBody]PaymentProcessRequest payment)
        {
            var paymentId = Guid.NewGuid();
            string status = "Accepted";

            if (payment.Amount == 0)
            {
                status = "Declined";
            }

            var response = new PaymentProcessResponse
            {
                PaymentId = paymentId,
                PaymentStatus = status
            };

            return Ok(response);
        }
    }
}