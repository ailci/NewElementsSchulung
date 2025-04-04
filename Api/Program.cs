using Api.Controllers;
using Api.Extensions;
using Api.Middleware;
using Logging;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.ConfigLoggingService();

builder.Services
    .ConfigureApi() //WebApi
    .ConfigureDb(builder.Configuration) //Db
    .ConfigureDependencyInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.################################################################
//app.Use(async (context, next) =>
//{
//    var userAgent = context.Request.Headers["User-Agent"][0];
//    await context.Response.WriteAsync($"User-Agent: {userAgent}");
//    await next();
//    await context.Response.WriteAsync("erste back middleware\n");
//});
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Zweite middleware\n");
//    await next();
//    await context.Response.WriteAsync("Zweite back middleware\n");
//});
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Ende middleware\n");
//});

//app.UseBrowserAllowedMiddleware(Browser.Chrome, Browser.Edge);

app.UseExceptionHandler(opt => { });

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //await app.ApplyMigrationAsync();

    //Scalar
    app.MapScalarApiReference();

    //Swagger
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Qotd Api v1"));
}

app.UseHttpsRedirection();

//app.UseApiKeyAuthMiddleware();  //1. Version via Middleware

app.UseAuthorization();

app.MapControllers();

app.Run();
