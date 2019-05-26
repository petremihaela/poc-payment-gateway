using PaymentService.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentService.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task<PaymentEntity> GetByIdAsync(Guid paymentId);

        Task CreateAsync(PaymentEntity payment);

    }
}