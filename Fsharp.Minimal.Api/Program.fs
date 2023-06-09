#nowarn "20"

open System.Reflection
open Fsharp.Minimal.Api
open Fsharp.Minimal.Api.EndpointProviders
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Hosting
open Microsoft.OpenApi.Models

let exitCode = 0

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)

    builder.Services.AddDbContext<ApiContext>(
        fun o -> o.UseInMemoryDatabase("ApiDb")
                |> ignore)

    builder.Services.AddEndpointsApiExplorer()
    builder.Services.AddSwaggerGen(
        fun o ->
            o.SwaggerDoc("v1", OpenApiInfo(
                Version = "v1",
                Title = "University API",
                Description =
                    "An API for managing universities and related resources. <br> \n\
                    Type: <b>F# Minimal</b>"))
            
            o.IncludeXmlComments(
                Assembly.GetExecutingAssembly()
                    .Location
                    .Replace(".dll", ".xml")))


    let app = builder.Build()
    
    if app.Environment.IsDevelopment() then
        app.UseSwagger()
        app.UseSwaggerUI()
        |> ignore
    
    StudentsEndpointProvider.registerEndpoints(app)
    TeachersEndpointProvider.registerEndpoints(app)
    
    app.Run()

    exitCode