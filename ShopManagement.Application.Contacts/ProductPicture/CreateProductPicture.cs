using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contacts.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contacts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1,long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public IFormFile Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
