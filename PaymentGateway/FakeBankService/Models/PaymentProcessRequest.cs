namespace FakeBankService.Models
{
    public class PaymentProcessRequest
    {
        public string CardNumber { get; set; }

        public string Ccv { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }
}