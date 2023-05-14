using Csharp.Minimal.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(
    o =>
        o.UseInMemoryDatabase("ApiDb"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();