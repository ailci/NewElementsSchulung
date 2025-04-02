using Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    #region Members/Constructors

    private readonly IServiceManager _serviceManager;
    private readonly ILoggerManager _logger;

    public AuthorsController(IServiceManager serviceManager, ILoggerManager logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
    }

    #endregion

    #region GET

    /// <summary>
    /// Gets all authors
    /// </summary>
    /// <returns>List of authors</returns>
    [HttpGet(Name = "GetAuthors")]
    public async Task<IActionResult> GetAuthors()
    {
        _logger.LogInformation($"{nameof(GetAuthors)} aufgerufen...");

        var authorDtos = await _serviceManager.AuthorService.GetAuthorsAsync();

        return Ok(authorDtos);
    }

    #endregion
}