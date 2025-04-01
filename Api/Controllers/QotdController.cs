using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.DataTransferObjects;

namespace Api.Controllers;

[Route("api/qotd")]   // => localhost:7050/api/qotd
[ApiController]
public class QotdController : ControllerBase
{
    private readonly QotdContext _context;

    public QotdController(QotdContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetQuoteOfTheDay() // => localhost:7050/api/qotd
    {
        var quote = _context.Quotes.Include(c => c.Author).First();

        var qotd = new QuoteOfTheDayDto
        {
            AuthorName = quote.Author?.Name ?? "",
            AuthorDescription = quote.Author?.Description ?? "",
            AuthorBirthDate = quote.Author?.BirthDate,
            QuoteText = quote.QuoteText
        };

        return Ok(qotd);
    }
}