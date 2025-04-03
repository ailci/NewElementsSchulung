using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.Validations;

namespace Shared.DataTransferObjects;

public class AuthorForCreateDto
{
    [Required(ErrorMessage = "Bitte geben Sie einen Namen ein")]
    [MaxLength(100, ErrorMessage = "Der Name darf 100 Zeichen nicht überschreiben")]
    [DeniedValues("administrator","root","admin","god", ErrorMessage = "Der Name ist nicht erlaubt")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Bitte geben Sie eine Description ein")]
    public required string Description { get; set; }

    [NoFutureDate(ErrorMessage = "Birthdate liegt in der Zukunft")]
    public DateOnly? BirthDate { get; set; }
    public IFormFile? Photo { get; set; }
    public IEnumerable<QuoteForCreateDto>? Quotes { get; set; }
}