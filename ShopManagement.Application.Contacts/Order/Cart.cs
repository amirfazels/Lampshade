using ShopManagement.Application.Contracts;

namespace ShopManagement.Application.Contacts.Order
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public int PaymentMethod { get; set; }
        public double PayAmount { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
            TotalAmount = 0;
            DiscountAmount = 0;
            PayAmount = 0;
        }
        public void Add(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount += cartItem.TotalItemPrice;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.ItemPayAmount;
        }

        public void SetPaymentMethod(int paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }
    }
}
