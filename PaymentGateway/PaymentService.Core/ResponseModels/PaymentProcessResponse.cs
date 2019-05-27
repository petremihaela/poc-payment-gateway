
using System;

namespace PaymentService.Core.ResponseModels
{
    public class PaymentProcessResponse
    {
        public Guid PaymentId { get; set; }

        public string PaymentStatus { get; set; }
    }
}
