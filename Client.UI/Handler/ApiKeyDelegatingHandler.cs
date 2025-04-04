using Microsoft.Extensions.Options;

namespace Client.UI.Handler;

public class ApiKeyDelegatingHandler(IOptions<QotdAppSettings> appSettings) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //ApiKey hinzufügen
        request.Headers.Add("x-api-key", appSettings.Value.XApiKey);

        //Anfrage absenden
        return base.SendAsync(request, cancellationToken);
    }
}