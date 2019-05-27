using System;

namespace FakeBankService.Models
{
    public class PaymentProcessResponse
    {
        public Guid PaymentId { get; set; }

        public string PaymentStatus { get; set; }
    }
}