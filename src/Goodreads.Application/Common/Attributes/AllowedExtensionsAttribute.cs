using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Goodreads.Application.Common.Attributes;
public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(ExtensionGroup group)
    {
        _extensions = group switch
        {
            ExtensionGroup.Image => AppConstants.AllowedImageExtensions,
            _ => throw new ArgumentOutOfRangeException(nameof(group), $"Unsupported extension group: {group}")
        };
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"File extension not allowed. Allowed: {string.Join(", ", _extensions)}");
            }
        }

        return ValidationResult.Success;
    }
}