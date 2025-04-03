using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Shared.Validations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class NoFutureDateAttribute : ValidationAttribute
{
    public NoFutureDateAttribute()
    {
        //Falls keine ErrorMessage gesetzt wurde, etzen wir den Standardwert
        if (string.IsNullOrWhiteSpace(ErrorMessage))
            ErrorMessage = "Datum liegt in der Zukunft";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        //var model = (AuthorForCreateDto) validationContext.ObjectInstance;

        if (value is DateOnly dateToCheck)
        {
            //Fehlerfall. Ausgewähltes Datum größer heute
            if (dateToCheck >= DateOnly.FromDateTime(DateTime.Today))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}