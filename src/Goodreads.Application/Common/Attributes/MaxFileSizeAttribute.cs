using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Goodreads.Application.Common.Attributes;
public class MaxFileSizeAttribute(long maxFileSizeInBytes) : ValidationAttribute
{


    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null && file.Length > maxFileSizeInBytes)
        {
            return new ValidationResult($"Max file size is {AppConstants.MaxFileSizeInMB} MB.");
        }

        return ValidationResult.Success;
    }
}