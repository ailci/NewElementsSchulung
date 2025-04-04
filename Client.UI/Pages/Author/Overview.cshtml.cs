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

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        try
        {
            var isDeleted = await qotdApiService.DeleteAuthorAsync(id);

            if (!isDeleted)
            {
                ErrorMessage = "Author konnte nicht gelöscht werden";
                return Page();
            }
                
            return RedirectToPage();
        }
        catch (HttpRequestException ex)
        {
            logger.LogError($"StatusCode: {ex.StatusCode} Message: {ex.Message}");
            ErrorMessage = ex.Message;
            return Page();
        }
    }
}