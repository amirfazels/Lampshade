using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contacts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";

        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            CartItems = _productQuery
                .CheckInventoryStatus
                (
                    serializer.Deserialize<List<CartItem>>(value) ?? new List<CartItem>()
                );
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            Response.Headers["Location"] = "/Cart";
            Response.StatusCode = 302;

            if (string.IsNullOrEmpty(value))
                return new EmptyResult();

            var cartItems = serializer.Deserialize<List<CartItem>>(value) ?? new List<CartItem>();
            var item = cartItems.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                cartItems.Remove(item);
                var options = new CookieOptions { 
                    Expires = DateTime.Now.AddDays(2),
                    Path = "/",
                    HttpOnly = false,
                    Secure = true,
                    IsEssential = true,
                };
                Response.Cookies.Delete(CookieName);
                Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            }

            return new EmptyResult();
        }
    }
}
