using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapFallback(requestDelegate: context =>
{
    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
    return context.Response.WriteAsync("Resource not found");
});

app.Run();

public partial class Program; // making class public for testing