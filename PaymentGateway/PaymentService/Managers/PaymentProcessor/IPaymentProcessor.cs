using PaymentService.Core.ReponseModels;
using PaymentService.Core.RequestModels;
using System.Threading.Tasks;

namespace PaymentService.Managers.PaymentProcessor
{
    public interface IPaymentProcessor
    {
        Task<PaymentProcessResponse> ProcessPaymentAsync(PaymentProcessRequest payment);
    }
}