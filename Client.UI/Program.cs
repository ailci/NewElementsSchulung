using Client.UI;
using Client.UI.Handler;
using Client.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//DI
builder.Services.Configure<QotdAppSettings>(builder.Configuration.GetSection("QotdAppSettings"));
builder.Services.AddScoped<IQotdApiService, QotdApiService>();
builder.Services.AddTransient<ApiKeyDelegatingHandler>();

var qotdAppSettings = builder.Configuration.GetSection("QotdAppSettings").Get<QotdAppSettings>();

//Named Http-Client
//builder.Services.AddHttpClient("qotdapiservice", client =>
//{
//    client.BaseAddress = new Uri(qotdAppSettings!.QotdApiServiceUri);
//    client.DefaultRequestHeaders.Add("Accept","application/json");
//});

//Typed Client
builder.Services.AddHttpClient<IQotdApiService, QotdApiService>(client =>
{
    client.BaseAddress = new Uri(qotdAppSettings!.QotdApiServiceUri);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    //client.DefaultRequestHeaders.Add("x-api-key", qotdAppSettings.XApiKey); // 2.Variante nicht ganz so hässlich
}).AddHttpMessageHandler<ApiKeyDelegatingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline. ##################################################################
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
