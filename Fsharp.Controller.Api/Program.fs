namespace Fsharp.Controller.Api
#nowarn "20"

open System.Reflection
open Microsoft.AspNetCore.Builder
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)
        
        builder.Services.AddDbContext<ApiContext>(
            fun o -> o.UseInMemoryDatabase("ApiDb")
                    |> ignore)

        builder.Services.AddControllers()
        builder.Services.AddSwaggerGen(
            fun o -> o.IncludeXmlComments(
                Assembly.GetExecutingAssembly()
                    .Location
                    .Replace(".dll", ".xml")))

        let app = builder.Build()

        if app.Environment.IsDevelopment() then
            app.UseSwagger()
            app.UseSwaggerUI()
            |> ignore
        
        app.UseHttpsRedirection()

        app.UseAuthorization()
        app.MapControllers()

        app.Run()

        exitCode