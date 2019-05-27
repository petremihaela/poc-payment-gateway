using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentService.Core.Models;
using PaymentService.Core.RequestModels;
using PaymentService.Core.ResponseModels;

namespace PaymentService.Core.Managers
{
    public interface IPaymentManager
    {
        Task<PaymentDetailsResponse> GetPaymentAsync(Guid paymentId);

        Task StorePaymentAsync(PaymentEntity payment);

        bool ValidatePaymentRequest(PaymentProcessRequest paymentRequest);

    }
}