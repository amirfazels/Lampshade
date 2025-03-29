using Newtonsoft.Json;

namespace _0_Framework.Application.ZarinPal
{
    public class PaymentResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public PaymentData? Data { get; set; } = null;

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public PaymentErrors? Errors { get; set; } = null;
    }

    public class PaymentErrors : object
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("validations")]
        public List<object> Validations { get; set; }
    }

    public class PaymentData : object
    {
        [JsonProperty("authority")]
        public string Authority { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
