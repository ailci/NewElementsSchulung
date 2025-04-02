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
public class QotdController(IServiceManager serviceManager) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetQuoteOfTheDayAsync() // => localhost:7050/api/qotd
    {
        return Ok(await serviceManager.QotdService.GetQuoteOfTheDayAsync());
    }
}