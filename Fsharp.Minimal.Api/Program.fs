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
    |> ignore
    
    let app = builder.Build()

    app.MapGet("/", Func<string>(fun () -> "Hello World!")) |> ignore

    app.Run()

    exitCode
