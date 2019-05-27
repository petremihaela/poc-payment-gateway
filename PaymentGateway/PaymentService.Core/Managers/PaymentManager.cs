using System;
using System.Threading.Tasks;
using PaymentService.Core.Helpers;
using PaymentService.Core.Models;
using PaymentService.Core.Repositories;
using PaymentService.Core.ResponseModels;

namespace PaymentService.Core.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentManager(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentDetailsResponse> GetPaymentAsync(Guid paymentId)
        {
            var paymentEntity = await _paymentRepository.GetByIdAsync(paymentId);

            if (paymentEntity == null)
                return null;

            var paymentDetails = PaymentConverter.ConvertEntityToDetailsResponse(paymentEntity);

            return paymentDetails;
        }

        public async Task StorePaymentAsync(PaymentEntity payment)
        {
            await _paymentRepository.CreateAsync(payment);
        }
    }
}