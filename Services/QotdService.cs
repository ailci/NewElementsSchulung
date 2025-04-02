using Persistence.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class QotdService : IQotdService
{
    private readonly IRepositoryManager _repositoryManager;

    public QotdService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<QuoteOfTheDayDto> GetQuoteOfTheDayAsync()
    {
        var quote = await _repositoryManager.QuoteRepo.GetRandomQuoteAsync();

        var qotd = new QuoteOfTheDayDto
        {
            Id = quote.Id,
            AuthorName = quote.Author?.Name ?? "",
            AuthorDescription = quote.Author?.Description ?? "",
            AuthorBirthDate = quote.Author?.BirthDate,
            AuthorPhoto = quote.Author?.Photo,
            AuthorPhotoMimeType = quote.Author?.PhotoMimeType,
            QuoteText = quote.QuoteText
        };

        return qotd;
    }
}