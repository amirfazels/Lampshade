using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contacts.ProductCategory;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contacts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public IFormFile? Picture { get; set; }
        public string? PictureAlt { get; set; }
        public string? PictureTitle { get; set; }
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }
        public ICollection<ProductCategoryViewModel> Categories { get; set; }
    }
}
