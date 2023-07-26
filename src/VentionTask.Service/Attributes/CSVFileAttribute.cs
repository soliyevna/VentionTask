using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentionTask.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CSVFileAttribute: ValidationAttribute
    {
        private readonly string[] _extensions = {".csv"};
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is null)
            {
                return new ValidationResult("Please upload a file.");
            }
            else if(file.Length <= 0)
            {
                return new ValidationResult("Make sure that the file is not empty!");
            }
            else 
            {
                var extention = Path.GetExtension(file.FileName);
                if (_extensions.Contains(extention.ToLower()))
                    return ValidationResult.Success;
                else return new ValidationResult("Please upload a file with csv extention");
            }
        }
    }
}
