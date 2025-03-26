using ShopManagement.Application.Contacts.Order;

namespace _01_LampshadeQuery.Contracts
{
    public interface ICartCalculatorService
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
