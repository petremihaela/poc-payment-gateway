namespace PaymentService.Core.RequestModels
{
    public class PaymentProcessRequest
    {
        public string CardNumber { get; set; }

        public string Ccv { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"CardNumber: {CardNumber}, CCV: {Ccv}, ExpiryYear: {ExpiryYear}, ExpiryMonth: {ExpiryMonth}, Currency: {Currency}, Amount: {Amount}";
        }
    }
}