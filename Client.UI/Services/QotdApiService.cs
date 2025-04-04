using Client.UI.Pages;
using Shared.DataTransferObjects;

namespace Client.UI.Services;

public class QotdApiService(ILogger<QotdApiService> logger, HttpClient client) 
    : IQotdApiService
{
    private const string QotdUri = "api/qotd";

    public async Task<QuoteOfTheDayDto?> GetQuoteOfTheDayAsync()
    {
        logger.LogInformation($"{nameof(GetQuoteOfTheDayAsync)} aufgerufen...");

        return await client.GetFromJsonAsync<QuoteOfTheDayDto>(QotdUri);
    }
}