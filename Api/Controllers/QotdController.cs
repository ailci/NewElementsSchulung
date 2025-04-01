using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        var authors = _context.Authors.ToList();

        var qotd = new QuoteOfTheDayDto
        {
            AuthorName = "Ich",
            AuthorDescription = "Dozent",
            AuthorBirthDate = new DateOnly(1978, 07, 13),
            QuoteText = "Larum Lierum"
        };

        return Ok(qotd);
    }
}