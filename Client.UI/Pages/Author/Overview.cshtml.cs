using Client.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.DataTransferObjects;
using Shared.Utilities;

namespace Client.UI.Pages.Author;

public class OverviewModel(ILogger<OverviewModel> logger, IQotdApiService qotdApiService) : PageModel
{
    public IEnumerable<AuthorDto>? AuthorDtos { get; set; }
    public string? ErrorMessage { get; set; }

    public async Task OnGet()
    {
        try
        {
            AuthorDtos = await qotdApiService.GetAuthorsAsync();
            logger.LogInformation($"Rückgabe GetAuthors: {AuthorDtos?.LogAsJson()}");
        }
        catch (HttpRequestException ex)
        {
            logger.LogError($"StatusCode: {ex.StatusCode} Message: {ex.Message}");
            ErrorMessage = ex.Message;
        }
    }
}