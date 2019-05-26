
using System;

namespace PaymentService.Core.ReponseModels
{
    public class PaymentProcessResponse
    {
        public Guid PaymentId { get; set; }

        public string PaymentStatus { get; set; }
    }
}
