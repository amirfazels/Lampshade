namespace ShopManagement.Application.Contacts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        string PaymentSucceeded(long orderId, long refId);
        double GetAmountById(long id);
    }
}
