using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace _0_Framework.Application
{
    public class FileExtentionLimitationAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string[] _FileExtentions;

        public FileExtentionLimitationAttribute(string[] fileExtentions)
        {
            _FileExtentions = fileExtentions;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage);
        }

        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtention = Path.GetExtension(file.FileName);
            return _FileExtentions.Contains(fileExtention);

        }
    }
}
