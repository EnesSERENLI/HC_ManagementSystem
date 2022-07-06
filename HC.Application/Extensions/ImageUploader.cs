using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HC.Application.Extensions
{
    public class ImageUploader : ValidationAttribute
    {
        string extension = "";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file != null)
            {
                extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg", "gif" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult(GetErrorMessage());
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Choise a image!");
        }

        private string GetErrorMessage()
        {
            return "Allowed extension are jpg png jpeg gif";
        }

        public string GetExtension()
        {
            return extension;
        }
    }
}
