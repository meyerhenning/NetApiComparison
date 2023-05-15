using Csharp.Minimal.Api;
using Csharp.Minimal.Api.EndpointProviders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(
    o =>
        o.UseInMemoryDatabase("ApiDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

StudentsEndpointProvider.RegisterEndpoints(app);

app.Run();