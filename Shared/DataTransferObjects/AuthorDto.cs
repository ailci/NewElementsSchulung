using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects;

public class AuthorDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public DateOnly? BirthDate { get; init; }
    public byte[]? Photo { get; init; }
    public string? PhotoMimeType { get; init; }
}