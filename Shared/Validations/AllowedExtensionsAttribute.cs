using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Validations;

//https://medium.com/@raza.sherazi514/title-custom-file-extension-validation-in-asp-net-core-f73441fa320f
//https://code-maze.com/aspnetcore-how-to-validate-file-upload-extensions/
public class AllowedExtensionsAttribute : ValidationAttribute
{
    private IEnumerable<string> AllowedExtensions { get; set; }

    public AllowedExtensionsAttribute(params string[] valideTypen)
    {
        AllowedExtensions = valideTypen.Select(c => c.Trim().ToLower());
        ErrorMessage = $"Nur folgende Datei-Typen sind erlaubt: {string.Join(",", AllowedExtensions)}";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile formFile && !AllowedExtensions.Any(c => formFile.FileName.EndsWith(c)))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;


        //Alternative Lösung
        //if (value is not IFormFile file) return ValidationResult.Success;

        //var extension = Path.GetExtension(file.FileName).Substring(1);

        //return AllowedExtensions.Contains(extension.ToLower())
        //    ? ValidationResult.Success
        //    : new ValidationResult($"Only files with the following fileExtensions are allowed: {string.Join(", ", AllowedExtensions)}");
    }
}