using Api.Filter;
using Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Services;
using Shared.DataTransferObjects;

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

        var authorDtos = await _serviceManager.AuthorService.GetAuthorsAsync(trackChanges: false);

        return Ok(authorDtos);
    }

    [HttpGet("{id:guid}", Name = "GetAuthor")]  // localhost:7050/api/authors/{id}
    //[OutputCache(Duration = 60)]
    [OutputCache(PolicyName = "120SecondsDuration", VaryByRouteValueNames = ["id"])]
    //[OutputCache(PolicyName = "RouteParamDuration")]
    public async Task<IActionResult> GetAuthor(Guid id)
    {
        var author = await _serviceManager.AuthorService.GetAuthorAsync(id);

        if (author is null) return NotFound();
        
        return Ok(author);
    }

    #endregion

    #region POST

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateAuthor(AuthorForCreateDto authorForCreateDto)
    {
        //if (!ModelState.IsValid) return BadRequest(ModelState); //braucht man nicht, wenn [ApiController] annotiert ist

        var authorDto = await _serviceManager.AuthorService.CreateAuthorAsync(authorForCreateDto);

        //return CreatedAtAction(nameof(GetAuthor), new { id = authorDto.Id }, authorDto);
        return CreatedAtRoute("GetAuthor", new { id = authorDto.Id }, authorDto);
    }

    #endregion

    #region DELETE

    [HttpDelete("{id:guid}", Name = "DeleteAuthor")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var isDeleted = await _serviceManager.AuthorService.DeleteAuthorAsync(id, trackChanges: false);

        if (!isDeleted) return Problem(detail: "Author konnte nicht gelöscht werden", statusCode: 501, title: "Error");
        
        return NoContent();
    }

    #endregion
}