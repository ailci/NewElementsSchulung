using Shared.DataTransferObjects;

namespace Client.UI.Services;

public interface IQotdApiService
{
    Task<QuoteOfTheDayDto?> GetQuoteOfTheDayAsync();
    Task<QuoteOfTheDayDto?> GetQuoteOfTheDaySecuredAsync();
    Task<IEnumerable<AuthorDto>?> GetAuthorsAsync();
    Task<bool> DeleteAuthorAsync(Guid authorId);
}