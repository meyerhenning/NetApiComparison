namespace Fsharp.Minimal.Api.EndpointHandlers

open System
open System.Threading.Tasks
open Fsharp.Minimal.Api
open Fsharp.Minimal.Api.Models
open Fsharp.Minimal.Api.Routes
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore

/// <summary>
/// Defines all teacher endpoint handlers.
/// </summary>
[<RequireQualifiedAccess>]
module TeacherEndpointHandlers =
    
    /// <summary>
    /// Adds a teacher.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="newTeacher">The teacher to add.</param>
    let addTeacher
        (context: ApiContext)
        ([<FromBody>] newTeacher: Teacher) : Task<IResult> =
        task{
            context.Teachers.Add(newTeacher)
            |> ignore
            
            context.SaveChangesAsync()
            |> Async.AwaitTask
            |> ignore
            
            return Results.Created(
                TeacherRoutes.Get
                    .Replace(
                        "{id:guid}",
                        newTeacher.Id.ToString("D")),
                newTeacher)
        }
    
    /// <summary>
    /// Deletes a teacher.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the teacher.</param>
    let deleteTeacher
        (context: ApiContext)
        ([<FromRoute>] id: Guid) : Task<IResult> =
        task{
            let! teacher =
                context.Teachers.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
            
            if obj.ReferenceEquals(teacher, null) then
                return Results.NotFound()
            else
                context.Teachers.Remove(teacher)
                |> ignore
                
                context.SaveChangesAsync()
                |> Async.AwaitTask
                |> ignore
                
                return Results.Ok()
        }
    
    /// <summary>
    /// Gets a single teacher by id.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the teacher.</param>
    let getTeacher
        (context: ApiContext)
        ([<FromRoute>] id: Guid) : Task<IResult> =
        task{
            let! teacher =
                context.Teachers.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
            
            if obj.ReferenceEquals(teacher, null) then
                return Results.NotFound()
            else
                return Results.Ok(teacher)
        }
    
    /// <summary>
    /// Gets all teachers.
    /// </summary>
    /// <param name="context">The db context.</param>
    let getAllTeachers
        (context: ApiContext) : Task<IResult> =
        task{
            let! teachers =
                context.Teachers.ToListAsync()
                |> Async.AwaitTask
            
            return Results.Ok(teachers)
        }
    
    /// <summary>
    /// Changes the properties of a teacher.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the teacher.</param>
    /// <param name="updatedTeacher">The teacher object with updated properties.</param>
    let updateTeacher
        (context: ApiContext)
        ([<FromRoute>] id : Guid)
        ([<FromBody>] updatedTeacher: Teacher) : Task<IResult> =
        task{
            if id <> updatedTeacher.Id then
                return Results.BadRequest()
            else
                let! teacher =
                    context.Teachers.FindAsync(id)
                        .AsTask()
                        |> Async.AwaitTask
                
                if obj.ReferenceEquals(teacher, null) then
                    return Results.NotFound()
                else
                    context.Teachers.Entry(teacher)
                        .CurrentValues.SetValues(updatedTeacher)
                    
                    context.SaveChangesAsync()
                    |> Async.AwaitTask
                    |> ignore
                    
                    return Results.Ok()
        }
