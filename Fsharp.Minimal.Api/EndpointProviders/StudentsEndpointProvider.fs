namespace Fsharp.Minimal.Api.EndpointProviders

open System
open System.Threading.Tasks
open Fsharp.Minimal.Api
open Fsharp.Minimal.Api.Models
open Fsharp.Minimal.Api.Routes
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.EntityFrameworkCore

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
        
        // Adds a student
        app.MapPost(
            StudentRoutes.Add,
            Func<ApiContext, Student, Task<IResult>>
                (fun context newStudent ->
                task{
                    context.Students.Add(newStudent)
                    |> ignore
                    
                    context.SaveChangesAsync()
                    |> Async.AwaitTask
                    |> ignore
                    
                    return Results.Created(
                        StudentRoutes.Get
                            .Replace(
                                "{id:guid}",
                                newStudent.Id.ToString("D")),
                        newStudent)
                }))
                .WithTags(EndpointBaseTag)
                |> ignore
        
        // Deletes a student
        app.MapDelete(
            StudentRoutes.Delete,
            Func<ApiContext, Guid, Task<IResult>>
                (fun context id ->
                task{
                    let! student =
                        context.Students.FindAsync(id)
                            .AsTask()
                            |> Async.AwaitTask
                    
                    if obj.ReferenceEquals(student, null) then
                        return Results.NotFound()
                    else
                        context.Students.Remove(student)
                        |> ignore
                        
                        context.SaveChangesAsync()
                        |> Async.AwaitTask
                        |> ignore
                        
                        return Results.Ok()
                }))
                .WithTags(EndpointBaseTag)
                |> ignore
        
        // Gets a single student by id.
        app.MapGet(
            StudentRoutes.Get,
            Func<ApiContext, Guid, Task<IResult>>
                (fun context id ->
                task{
                    let! student =
                        context.Students.FindAsync(id)
                            .AsTask()
                            |> Async.AwaitTask
                    
                    if obj.ReferenceEquals(student, null) then
                        return Results.NotFound()
                    else
                        return Results.Ok(student)
                }))
                .WithTags(EndpointBaseTag)
                |> ignore
        
        // Gets all students.
        app.MapGet(
            StudentRoutes.GetAll,
            Func<ApiContext, Task<IResult>>
                (fun context ->
                task{
                    let! students =
                        context.Students.ToListAsync()
                        |> Async.AwaitTask
                    
                    return Results.Ok(students)
                }))
                .WithTags(EndpointBaseTag)
                |> ignore
        
        // Updates a student.
        app.MapPut(
            StudentRoutes.Update,
            Func<ApiContext, Guid, Student, Task<IResult>>
                (fun context id updatedStudent ->
                task{
                    if id <> updatedStudent.Id then
                        return Results.BadRequest()
                    else
                        let! student =
                            context.Students.FindAsync(id)
                                .AsTask()
                                |> Async.AwaitTask
                        
                        if obj.ReferenceEquals(student, null) then
                            return Results.NotFound()
                        else
                            context.Students.Entry(student)
                                .CurrentValues.SetValues(updatedStudent)
                            
                            context.SaveChangesAsync()
                            |> Async.AwaitTask
                            |> ignore
                            
                            return Results.Ok()
                }))
                .WithTags(EndpointBaseTag)
                |> ignore