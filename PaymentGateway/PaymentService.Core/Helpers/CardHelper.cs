using System;
using System.Linq;
using PaymentService.Core.Constants;

namespace PaymentService.Core.Helpers
{
    public static class CardHelper
    {
        /// <summary>
        /// Card number is masked without the last <see cref="CardValidationConstants.CardNumberUnmaskedDigitsNo"/> digits
        /// </summary>
        public static string Mask(string cardNumber)
        {
            var maskedCardNumber = cardNumber.Substring(cardNumber.Length - CardValidationConstants.CardNumberUnmaskedDigitsNo).PadLeft(cardNumber.Length, '*');
            return maskedCardNumber;
        }

        /// <summary>
        /// According with ISO/IEC 7812 - payment card numbers are
        /// composed of <see cref="CardValidationConstants.CardNumberMinLength"/>   to <see cref="CardValidationConstants.CardNumberMaxLength"/> digits
        /// </summary>
        public static bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                throw new ArgumentException();

            if (cardNumber.Length < CardValidationConstants.CardNumberMinLength || cardNumber.Length > CardValidationConstants.CardNumberMaxLength || !cardNumber.All(char.IsDigit))
                throw new FormatException();

            return true;
        }

        /// <summary>
        /// CCV field has exactly <see cref="CardValidationConstants.CardCcvLength"/> digits
        /// </summary>
        public static bool IsValidCcv(string ccv)
        {
            if (string.IsNullOrEmpty(ccv))
                throw new ArgumentException();

            if (ccv.Length != CardValidationConstants.CardCcvLength || !ccv.All(char.IsDigit))
                throw new FormatException();

            return true;
        }

        public static bool IsValidExpiryMonth(int expiryMonth)
        {
            var isValidExpiryMonth = expiryMonth >= 1 && expiryMonth <= 12;
            return isValidExpiryMonth;
        }
    }
}