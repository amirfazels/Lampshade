using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contacts.Order;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";

        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(IProductQuery productQuery, ICartCalculatorService cartCalculatorService, ICartService cartService)
        {
            _cartService = cartService;
            _productQuery = productQuery;
            _cartCalculatorService = cartCalculatorService;
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
            return RedirectToPage(result.Any(x => !x.IsInStock) ? "/Cart" : "/Checkout");
        }
    }
}
