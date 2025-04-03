using Persistence.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Logging;

namespace Services;

public class QotdService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper) 
    : IQotdService
{
    public async Task<QuoteOfTheDayDto> GetQuoteOfTheDayAsync(bool trackChanges)
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} aufgerufen...");

        var quote = await repositoryManager.QuoteRepo.GetRandomQuoteAsync(trackChanges);

        //var qotd = new QuoteOfTheDayDto
        //{
        //    Id = quote.Id,
        //    AuthorName = quote.Author?.Name ?? "",
        //    AuthorDescription = quote.Author?.Description ?? "",
        //    AuthorBirthDate = quote.Author?.BirthDate,
        //    AuthorPhoto = quote.Author?.Photo,
        //    AuthorPhotoMimeType = quote.Author?.PhotoMimeType,
        //    QuoteText = quote.QuoteText
        //};

        return mapper.Map<QuoteOfTheDayDto>(quote);
    }
}