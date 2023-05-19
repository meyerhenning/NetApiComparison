using System.Reflection;
using Csharp.Controller.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(
    o =>
        o.UseInMemoryDatabase("ApiDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    o =>
        o.IncludeXmlComments(
            Assembly.GetExecutingAssembly()
                .Location
                .Replace(".dll", ".xml")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();