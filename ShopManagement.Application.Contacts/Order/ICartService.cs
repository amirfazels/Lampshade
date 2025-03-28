namespace ShopManagement.Application.Contacts.Order
{
    public interface ICartService
    {
        Cart Get();
        void Set(Cart cart);
    }
}
