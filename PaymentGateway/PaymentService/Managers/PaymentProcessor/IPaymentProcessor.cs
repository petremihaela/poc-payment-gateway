using PaymentService.Core.RequestModels;
using System.Threading.Tasks;
using PaymentService.Core.ResponseModels;

namespace PaymentService.Managers.PaymentProcessor
{
    public interface IPaymentProcessor
    {
        Task<PaymentProcessResponse> ProcessPaymentAsync(PaymentProcessRequest payment);
    }
}