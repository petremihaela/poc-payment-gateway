using FakeBankService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FakeBankService.Controllers
{
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult ProcessPayment([FromBody]PaymentProcessRequest payment)
        {
            var paymentId = Guid.NewGuid();
            string status = "";

            if (payment.Amount  == 0 )
            {
                status = "Declined";
            }
            return Ok(new { paymentId, status });
        }
    }
}