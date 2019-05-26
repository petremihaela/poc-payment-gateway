namespace PaymentService.Core.Helpers
{
    public static class CardHelper
    {
        public static string Mask(string cardNumber)
        {
            return cardNumber.Substring(cardNumber.Length - 4).PadLeft(cardNumber.Length, '*');
        }
    }
}