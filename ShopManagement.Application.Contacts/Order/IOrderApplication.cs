namespace ShopManagement.Application.Contacts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        string PaymentSucceeded(long orderId, long refId);
        void Restore(long orderId);
        void Cancel(long orderId);
        double GetAmountById(long id);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
