namespace _0_Framework.Application.ZarinPal
{
    public class PaymentRequest
    {
        public string merchant_id { get; set; }
        public int amount { get; set; }
        public string? currency { get; set; }
        public string description { get; set; }
        public string callback_url { get; set; }
        public PaymentRequestMetadata? metadata { get; set; }
    }
}
