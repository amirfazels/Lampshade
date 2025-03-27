namespace ShopManagement.Application.Contacts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
    }
}
