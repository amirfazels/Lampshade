using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Extensions;
using Nancy.Json;
using ShopManagement.Application.Contacts.Order;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";

        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(IProductQuery productQuery, ICartCalculatorService cartCalculatorService,
            ICartService cartService, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory,
            IAuthHelper authHelper)
        {
            _authHelper = authHelper;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _cartCalculatorService = cartCalculatorService;
            _zarinPalFactory = zarinPalFactory;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            var cartItems = _productQuery
                .CheckInventoryStatus
                (
                    serializer.Deserialize<List<CartItem>>(value) ?? new List<CartItem>()
                );

            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);
            var result = _productQuery.CheckInventoryStatus(cart.Items);

            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var AccountMobile = _authHelper.CurrentAccountMobile();
            var AccountUsername = _authHelper.CurrentAccountInfo().Username;
            var orderId = _orderApplication.PlaceOrder(cart);

            if (paymentMethod != 1)
            {
                var paymentResult = new PaymentResult()
                    .OfflinePayment("سفارش شما با موفقیت انجام شد، پس پرداخت وجه و تماس همکاران کالا ارسال می شود");
                return RedirectToPage("/PaymentResult", paymentResult);
            }

            PaymentResponse paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), AccountMobile, AccountUsername, "خرید ار فروشگاه لامپ شیدا", orderId);
            var paymentResponseData = paymentResponse.Data;
            if (paymentResponseData.Code.Equals(100))
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{paymentResponseData.Authority}");

            return Content(new JavaScriptSerializer().Serialize(paymentResponse), "application/json");
        }


        public IActionResult OnGetCallBack([FromQuery]string authority, [FromQuery] string status, [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountById(oId);
            var verificationResponse = _zarinPalFactory
                .CreateVerificationRequest(authority, 
                    orderAmount.ToString(CultureInfo.InvariantCulture));
            var paymentResult = new PaymentResult();
            if (status.ToLower().Equals("ok") && verificationResponse.Data.IsSuccess)
            {
                string issueTrackingNo = _orderApplication.PaymentSucceeded(oId, verificationResponse.Data.RefId);
                Response.Cookies.Delete(CookieName);
                paymentResult = paymentResult.Succeeded("پرداخت با موفقیت انجام شد", issueTrackingNo);
            }
            else
                paymentResult  = paymentResult.Failed("پرداخت با موفقیت انجام نشد");

            return RedirectToPage("/PaymentResult", paymentResult); 
        }
    }
}
