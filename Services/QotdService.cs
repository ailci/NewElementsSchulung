using Persistence.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logging;

namespace Services;

public class QotdService : IQotdService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;

    public QotdService(IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
    }

    public async Task<QuoteOfTheDayDto> GetQuoteOfTheDayAsync()
    {
        _logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} aufgerufen...");

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