using _0_Framework.Application;
using ShopManagement.Application.Contacts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Applicatiom.Contract.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, 100, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
