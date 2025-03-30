using Newtonsoft.Json;

namespace _0_Framework.Application.ZarinPal
{
    public class VerificationResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public VerificationData? Data { get; set; } = null;

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public VerificationErrors? Errors { get; set; } = null;
    }

    public class VerificationErrors
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class VerificationData
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("ref_id")]
        public long RefId { get; set; }

        [JsonProperty("card_pan")]
        public string MaskedCardNumber { get; set; }

        [JsonProperty("card_hash")]
        public string CardFingerprint { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("shaparak_fee")]
        public int ShaparakFee { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public bool IsSuccess => Code == 100 || Code == 101;
    }
}