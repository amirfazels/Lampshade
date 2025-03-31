using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace _0_Framework.Application.ZarinPal
{
    public class ZarinPalFactory : IZarinPalFactory
    {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get; }

        public ZarinPalFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            Prefix = _configuration.GetSection("payment")["method"];
            MerchantId = _configuration.GetSection("payment")["merchant"];
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
            long orderId)
        {
            amount = amount.Replace(",", "");
            var finalAmount = long.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/request.json");
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");

            var paymentRequestMetadata = new PaymentRequestMetadata();

            if (!string.IsNullOrWhiteSpace(mobile))
                paymentRequestMetadata.SetMobile(mobile);

            if (!string.IsNullOrWhiteSpace(email))
                paymentRequestMetadata.SetEmail(email);

            if (orderId != 0)
                paymentRequestMetadata.SetOrderId(orderId);



            var body = new PaymentRequest
            {
                merchant_id = MerchantId,
                amount = finalAmount,
                currency = ZarinpalCurrency.IranianToman,
                description = description,
                callback_url = $"{siteUrl}/Checkout?handler=CallBack&oId={orderId}"
            };

            body.metadata = paymentRequestMetadata;

            request.AddJsonBody(body);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<PaymentResponse>(jsonResponse(response));
        }


        public VerificationResponse CreateVerificationRequest(string authority, string amount)
        {
            var client = new RestClient($"https://{Prefix}.zarinpal.com/pg/v4/payment/verify.json");
            var request = new RestRequest();
            request.Method = Method.Post; 
            request.AddHeader("Content-Type", "application/json");

            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);

            request.AddJsonBody(new VerificationRequest
            {
                amount = finalAmount,
                merchant_id = MerchantId,
                authority = authority
            });
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<VerificationResponse>(jsonResponse(response));
        }

        private string jsonResponse(RestResponse? response)
        {
            return response.Content.Replace("\"errors\":[]", "\"errors\":{}");
        }
    }
}