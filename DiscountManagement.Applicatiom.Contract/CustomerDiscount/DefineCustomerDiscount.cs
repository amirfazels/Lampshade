using ShopManagement.Application.Contacts.Product;

namespace DiscountManagement.Applicatiom.Contract.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
