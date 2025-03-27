namespace ShopManagement.Application.Contacts.Order
{
    public class AddOrderItem
    {
        public long ProductId { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public double DiscountRate { get; set; }
    }
}
