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
/// Defines all student endpoint handlers.
/// </summary>
[<RequireQualifiedAccess>]
module StudentEndpointHandlers =
    
    /// <summary>
    /// Adds a student.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="newStudent">The student to add.</param>
    let addStudent
        (context: ApiContext)
        ([<FromBody>] newStudent: Student) : Task<IResult> =
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
        }
    
    /// <summary>
    /// Deletes a student.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the student.</param>
    let deleteStudent
        (context: ApiContext)
        ([<FromRoute>] id: Guid) : Task<IResult> =
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
        }
    
    /// <summary>
    /// Gets a single student by id.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the student.</param>
    let getStudent
        (context: ApiContext)
        ([<FromRoute>] id: Guid) : Task<IResult> =
        task{
            let! student =
                context.Students.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
            
            if obj.ReferenceEquals(student, null) then
                return Results.NotFound()
            else
                return Results.Ok(student)
        }
    
    /// <summary>
    /// Gets all students.
    /// </summary>
    /// <param name="context">The db context.</param>
    let getAllStudents
        (context: ApiContext) : Task<IResult> =
        task{
            let! students =
                context.Students.ToListAsync()
                |> Async.AwaitTask
            
            return Results.Ok(students)
        }
    
    /// <summary>
    /// Changes the properties of a student.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the student.</param>
    /// <param name="updatedStudent">The student object with updated properties.</param>
    let updateStudent
        (context: ApiContext)
        ([<FromRoute>] id : Guid)
        ([<FromBody>] updatedStudent: Student) : Task<IResult> =
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
        }