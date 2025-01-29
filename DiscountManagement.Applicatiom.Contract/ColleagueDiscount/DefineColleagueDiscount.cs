using ShopManagement.Application.Contacts.Product;

namespace DiscountManagement.Applicatiom.Contract.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
