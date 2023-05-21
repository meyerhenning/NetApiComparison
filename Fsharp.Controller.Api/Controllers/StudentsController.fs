namespace Fsharp.Controller.Api.Controllers

open System
open Fsharp.Controller.Api
open Fsharp.Controller.Api.Models
open Fsharp.Controller.Api.Routes
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore

/// <summary>
/// Defines the controller for managing students.
/// </summary>
[<ApiController>]
[<Route(StudentRoutes.Base)>]
type StudentsController(context: ApiContext) =
    inherit ControllerBase()
    
    /// <summary>
    /// Adds a new student.
    /// </summary>
    /// <param name="newStudent">The student to add.</param>
    [<HttpPost>]
    [<Route(StudentRoutes.Add)>]
    [<ProducesResponseType(typedefof<Student>, StatusCodes.Status201Created)>]
    member x.Add(
        [<FromBody>] newStudent: Student) : Async<IActionResult> =
        async {
            context.Students.Add(newStudent)
            |> ignore
            
            context.SaveChangesAsync()
            |> Async.AwaitTask
            |> ignore
            
            return x.CreatedAtAction(
                nameof x.Get,
                {| id = newStudent.Id |},
                newStudent)
        }
    
    /// <summary>
    /// Deletes a student.
    /// </summary>
    /// <param name="id">The id of the student.</param>
    [<HttpDelete(StudentRoutes.Delete)>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member x.Delete(
        [<FromRoute>] id: Guid) : Async<IActionResult> =
        async {
            let! student =
                context.Students.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
            
            if obj.ReferenceEquals(student, null) then
                return x.NotFound()
            else
                context.Students.Remove(student)
                |> ignore
                
                context.SaveChangesAsync()
                |> Async.AwaitTask
                |> ignore
                
                return x.Ok()
        }
        
    /// <summary>
    /// Gets a student.
    /// </summary>
    /// <param name="id">The id of the student.</param>
    [<HttpGet(StudentRoutes.Get)>]
    [<ProducesResponseType(typedefof<Student>, StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member x.Get(
        [<FromRoute>] id: Guid) : Async<IActionResult> =
        async {
            let! student =
                context.Students.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
                
            if obj.ReferenceEquals(student, null) then
                return x.NotFound()
            else
                return x.Ok(student)
        }
        
    /// <summary>
    /// Gets all students.
    /// </summary>
    [<HttpGet(StudentRoutes.GetAll)>]
    [<ProducesResponseType(typedefof<List<Student>>, StatusCodes.Status200OK)>]
    member x.GetAll() : Async<IActionResult> =
        async {
            let! students =
                context.Students.ToListAsync()
                |> Async.AwaitTask
                
            return x.Ok(students)
        }
    
    /// <summary>
    /// Changes the properties of a student.
    /// </summary>
    /// <param name="id">The id of the student.</param>
    /// <param name="updatedStudent">The updated student object.</param>
    [<HttpPut(StudentRoutes.Update)>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status400BadRequest)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member x.Update(
        [<FromRoute>] id: Guid,
        [<FromBody>] updatedStudent: Student) : Async<IActionResult> =
        async{
            if id <> updatedStudent.Id then
                return x.BadRequest()
            else
                
                let! student =
                    context.Students.FindAsync(id)
                        .AsTask()
                        |> Async.AwaitTask
                
                if obj.ReferenceEquals(student, null) then
                    return x.NotFound()
                else
                    context.Students.Entry(student)
                        .CurrentValues.SetValues(updatedStudent)
                    
                    context.SaveChangesAsync()
                    |> Async.AwaitTask
                    |> ignore
                    
                    return x.Ok()
        }