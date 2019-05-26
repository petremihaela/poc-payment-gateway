using PaymentService.Core.Models;
using PaymentService.Core.ReponseModels;
using System;
using System.Threading.Tasks;

namespace PaymentService.Core.Services
{
    public interface IPaymentManager
    {
        Task<PaymentDetailsResponse> GetPaymentAsync(Guid paymentId);

        Task StorePaymentAsync(PaymentEntity payment);
    }
}