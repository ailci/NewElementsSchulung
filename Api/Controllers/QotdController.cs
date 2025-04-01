using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;

namespace Api.Controllers;

[Route("api/qotd")]   // => localhost:7050/api/qotd
[ApiController]
public class QotdController : ControllerBase
{
    [HttpGet]
    public IActionResult GetQuoteOfTheDay() // => localhost:7050/api/qotd
    {
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