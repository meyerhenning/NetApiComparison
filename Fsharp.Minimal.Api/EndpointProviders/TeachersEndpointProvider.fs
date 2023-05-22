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
/// Represents the provider of all teacher endpoints.
/// </summary>
[<RequireQualifiedAccess>]
module TeachersEndpointProvider =
    
    [<Literal>]
    let private EndpointBaseTag = "Teachers"
    
    /// <summary>
    /// Registers all teacher endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    let registerEndpoints (app: WebApplication) =
        
        app.MapPost(
                TeacherRoutes.Add,
                Func<ApiContext, Teacher, Task<IResult>>(
                    TeacherEndpointHandlers.addTeacher))
            .Produces<Teacher>(
                StatusCodes.Status201Created,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapDelete(
                TeacherRoutes.Delete,
                Func<ApiContext, Guid, Task<IResult>>(
                    TeacherEndpointHandlers.deleteTeacher))
            .Produces(
                StatusCodes.Status200OK,
                MediaTypeNames.Application.Json)
            .Produces(
                StatusCodes.Status404NotFound,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapGet(
                TeacherRoutes.Get,
                Func<ApiContext, Guid, Task<IResult>>(
                    TeacherEndpointHandlers.getTeacher))
            .Produces<Teacher>(
                StatusCodes.Status200OK,
                MediaTypeNames.Application.Json)
            .Produces(
                StatusCodes.Status404NotFound,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapGet(
                TeacherRoutes.GetAll,
                Func<ApiContext, Task<IResult>>(
                    TeacherEndpointHandlers.getAllTeachers))
            .Produces<List<Teacher>>(
                StatusCodes.Status200OK,
                MediaTypeNames.Application.Json)
            .WithTags(EndpointBaseTag)
            |> ignore
        
        app.MapPut(
            TeacherRoutes.Update,
            Func<ApiContext, Guid, Teacher, Task<IResult>>(
                TeacherEndpointHandlers.updateTeacher))
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

