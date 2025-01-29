using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Applicatiom.Contract.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, 100, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
