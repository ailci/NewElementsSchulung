using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Contracts;
using Shared.DataTransferObjects;

namespace Api.Controllers;

[Route("api/qotd")]   // => localhost:7050/api/qotd
[ApiController]
public class QotdController : ControllerBase
{
    private readonly IRepositoryManager _repositoryManager;

    public QotdController(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuoteOfTheDayAsync() // => localhost:7050/api/qotd
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

        return Ok(qotd);
    }
}