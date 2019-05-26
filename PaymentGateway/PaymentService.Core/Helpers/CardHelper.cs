using System;
using System.Linq;

namespace PaymentService.Core.Helpers
{
    public static class CardHelper
    {
        public static string Mask(string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 4).PadLeft(cardNumber.Length, '*');
        }

        /// <summary>
        /// According with ISO/IEC 7812 - payment card numbers are composed of 8 to 19 digits
        /// </summary>
        public static bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber))
                throw new ArgumentException();

            if (cardNumber.Length < 8 || cardNumber.Length > 19 || !cardNumber.All(char.IsDigit))
                throw new FormatException();

            return true;
        }
    }
}