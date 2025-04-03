namespace Shared.DataTransferObjects;

public class QuoteForCreateDto
{
    public required string QuoteText { get; set; }
    public Guid AuthorId { get; set; }
}