using PaymentService.Core.Helpers;
using PaymentService.Core.Models;
using PaymentService.Core.Repositories;
using PaymentService.Core.RequestModels;
using PaymentService.Core.ResponseModels;
using System;
using System.Threading.Tasks;

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

        public bool ValidatePaymentRequest(PaymentProcessRequest paymentRequest)
        {
            if (paymentRequest == null)
                throw new ArgumentNullException();

            var isValidCardNumber = CardHelper.IsValidCardNumber(paymentRequest.CardNumber);
            var isValidCcv = CardHelper.IsValidCcv(paymentRequest.Ccv);
            var isValidMonth = CardHelper.IsValidExpiryMonth(paymentRequest.ExpiryMonth);

            return isValidCardNumber && isValidCcv && isValidMonth;
        }
    }
}