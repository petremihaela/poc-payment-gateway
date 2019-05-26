namespace PaymentService.Core.ReponseModels
{
    public class PaymentDetailsResponse
    {
        public string MaskedCardNumber { get; set; }

        public string Ccv { get; set; }

        public int ExpiryYear { get; set; }

        public int ExpiryMonth { get; set; }

        public string PaymentStatus { get; set; }
    }
}