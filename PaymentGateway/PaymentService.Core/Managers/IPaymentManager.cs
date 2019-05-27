using System;
using System.Threading.Tasks;
using PaymentService.Core.Models;
using PaymentService.Core.ResponseModels;

namespace PaymentService.Core.Managers
{
    public interface IPaymentManager
    {
        Task<PaymentDetailsResponse> GetPaymentAsync(Guid paymentId);

        Task StorePaymentAsync(PaymentEntity payment);
    }
}