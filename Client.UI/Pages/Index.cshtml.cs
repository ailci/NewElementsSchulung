using System.Text.Json;
using Client.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.DataTransferObjects;

namespace Client.UI.Pages;

public class IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IQotdApiService qotdApiService) 
    : PageModel
{
    public QuoteOfTheDayDto? QotdDto { get; set; }

    public async Task OnGetAsync()
    {
        try
        {
            //1. Version Klassik
            //var client = httpClientFactory.CreateClient("qotdapiservice");
            //var response = await client.GetAsync("api/qotd");  // BaseAddress + Individuell => localhost:7050/api/qotd
            
            //response.EnsureSuccessStatusCode();
            //var content = await response.Content.ReadAsStringAsync();
            //QotdDto = JsonSerializer.Deserialize<QuoteOfTheDayDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            //2. Version Abkürzung
            //var client = httpClientFactory.CreateClient("qotdapiservice");
            //QotdDto = await client.GetFromJsonAsync<QuoteOfTheDayDto>("api/qotd");

            //3. Version via Service
            QotdDto = await qotdApiService.GetQuoteOfTheDayAsync();
        }
        catch (HttpRequestException ex)
        {
           logger.LogError($"StatusCode: {ex.StatusCode} Message: {ex.Message}");
        }
    }
}