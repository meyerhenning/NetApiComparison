#nowarn "20"

open Fsharp.Minimal.Api
open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Hosting
let exitCode = 0

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)

    builder.Services.AddDbContext<ApiContext>(
        fun o -> o.UseInMemoryDatabase("ApiDb")
                |> ignore)

    builder.Services.AddEndpointsApiExplorer()
    builder.Services.AddSwaggerGen()

    let app = builder.Build()

    if app.Environment.IsDevelopment() then
        app.UseSwagger()
        app.UseSwaggerUI()
        |> ignore
    
    app.MapGet("/", Func<string>(fun () -> "Hello World!")) |> ignore
    
    app.Run()

    exitCode