using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using DiscountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contacts.Order;

namespace _01_LampshadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly IAuthHelper _authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {

            var cart = new Cart();

            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemove)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();


            var currentAccountRole = _authHelper.CurrentAccountRole();

            var applicableDiscounts = currentAccountRole == Roles.Colleague ? colleagueDiscounts : customerDiscounts;

            foreach (var cartItem in cartItems)
            {
                var discount = applicableDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                if (discount != null)
                    cartItem.DiscountRate = discount.DiscountRate ;
                cart.Add(cartItem);
            }

            return cart;
        }
    }
}
