var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();  //WebApi

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
