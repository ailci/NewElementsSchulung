using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects;

public class AuthorForCreateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly? BirthDateOnly { get; set; }
    public IFormFile Photo { get; set; }
    public IEnumerable<QuoteForCreateDto>? Quotes { get; set; }
}