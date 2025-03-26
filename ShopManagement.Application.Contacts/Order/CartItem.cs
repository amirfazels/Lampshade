namespace ShopManagement.Application.Contacts.Order
{
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public bool IsInStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount => (TotalItemPrice * DiscountRate) / 100;
        public double ItemPayAmount => TotalItemPrice - DiscountAmount;
        public double TotalItemPrice => UnitPrice * Count;
    }
}
