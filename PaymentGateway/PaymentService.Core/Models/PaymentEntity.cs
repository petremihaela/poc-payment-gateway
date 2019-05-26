using System;

namespace PaymentService.Core.Models
{
    public class PaymentEntity
    {
        public Guid PaymentId { get; set; }

        public string CardNumber { get; set; }

        public string Ccv { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}