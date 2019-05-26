using PaymentService.Core.Models;
using PaymentService.Core.ReponseModels;
using PaymentService.Core.RequestModels;

namespace PaymentService.Core.Helpers
{
    public static class PaymentConverter
    {
        public static PaymentEntity ConvertRequestToEntity(PaymentProcessRequest request)
        {
            var paymentEntity = new PaymentEntity
            {
                CardNumber = request.CardNumber,
                Ccv = request.Ccv,
                ExpiryYear = request.ExpiryYear,
                ExpiryMonth = request.ExpiryMonth,
                Currency = request.Currency,
                Amount = request.Amount
            };

            return paymentEntity;
        }

        public static PaymentDetailsResponse ConvertEntityToDetailsResponse(PaymentEntity entity)
        {
            var maskedCardNumber = CardHelper.Mask(entity.CardNumber);

            var paymentDetails = new PaymentDetailsResponse
            {
                MaskedCardNumber = maskedCardNumber,
                Ccv = entity.Ccv,
                ExpiryMonth = entity.ExpiryMonth,
                ExpiryYear = entity.ExpiryYear,
                PaymentStatus = entity.PaymentStatus
            };

            return paymentDetails;
        }
    }
}