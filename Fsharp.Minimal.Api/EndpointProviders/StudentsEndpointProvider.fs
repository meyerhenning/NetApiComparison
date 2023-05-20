namespace Fsharp.Minimal.Api.EndpointProviders

open System
open System.Net.Mime
open System.Threading.Tasks
open Fsharp.Minimal.Api
open Fsharp.Minimal.Api.EndpointHandlers
open Fsharp.Minimal.Api.Models
open Fsharp.Minimal.Api.Routes
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

/// <summary>
/// Represents the provider of all student endpoints.
/// </summary>
[<RequireQualifiedAccess>]
module StudentsEndpointProvider =
    
    [<Literal>]
    let private EndpointBaseTag = "Students"
    
    /// <summary>
    /// Registers all student endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    let registerEndpoints (app: WebApplication) =
        
        app.MapPost(
                StudentRoutes.Add,
                Func<ApiContext, Student, Task<IResult>>(
                    StudentEndpointHandlers.addStudent))
            .Produces<Student>(
                StatusCodes.Status201Created,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapDelete(
                StudentRoutes.Delete,
                Func<ApiContext, Guid, Task<IResult>>(
                    StudentEndpointHandlers.deleteStudent))
            .Produces(
                StatusCodes.Status200OK,
                MediaTypeNames.Application.Json)
            .Produces(
                StatusCodes.Status404NotFound,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapGet(
                StudentRoutes.Get,
                Func<ApiContext, Guid, Task<IResult>>(
                    StudentEndpointHandlers.getStudent))
            .Produces<Student>(
                StatusCodes.Status200OK,
                MediaTypeNames.Application.Json)
            .Produces(
                StatusCodes.Status404NotFound,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapGet(
                StudentRoutes.GetAll,
                Func<ApiContext, Task<IResult>>(
                    StudentEndpointHandlers.getAllStudents))
            .Produces<List<Student>>(
                StatusCodes.Status200OK,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapPut(
            StudentRoutes.Update,
            Func<ApiContext, Guid, Student, Task<IResult>>(
                StudentEndpointHandlers.updateStudent))
                .Produces(
                    StatusCodes.Status200OK,
                    MediaTypeNames.Application.Json)
                .Produces(
                    StatusCodes.Status400BadRequest,
                    MediaTypeNames.Application.Json)
                .Produces(
                    StatusCodes.Status404NotFound,
                    MediaTypeNames.Application.Json)
                .WithTags(EndpointBaseTag)
                |> ignore