using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contacts.Slide
{
    public class CreateSlide
    {
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        [FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Heading { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string BtnText { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Link { get; set; }
    }
}
