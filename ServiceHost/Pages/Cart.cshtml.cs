using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contacts.Order;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            CartItems = serializer.Deserialize<List<CartItem>>(value) ?? new List<CartItem>();
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            //if (string.IsNullOrEmpty(value))
            //    return RedirectToPage("/Cart");

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
                    IsEssential = true, //<- there
                };
                Response.Cookies.Delete(CookieName);
                Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            }
            Response.Headers["Location"] = "/Cart";
            Response.StatusCode = 302;
            return new EmptyResult();
        }
    }
}
