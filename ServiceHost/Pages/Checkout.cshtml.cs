using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Extensions;
using Nancy.Json;
using Nancy.ViewEngines;
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

        public IActionResult OnPostPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.Items);

            if (result.Any(x => !x.IsInStock))
                return RedirectToPage("/Cart");

            var AccountMobile = _authHelper.CurrentAccountMobile();
            var AccountUsername = _authHelper.CurrentAccountInfo().Username;
            var orderId = _orderApplication.PlaceOrder(cart);
            PaymentResponse paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), AccountMobile, AccountUsername, "خرید ار فروشگاه لامپ شیدا", orderId);
            var paymentResponseData = (PaymentData)paymentResponse.Data;
            if (paymentResponseData.Code.Equals(100))
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{paymentResponseData.Authority}");

            return Content(new JavaScriptSerializer().Serialize(paymentResponse), "application/json");
        }
    }
}
