namespace Fsharp.Controller.Api.Controllers

open System
open Fsharp.Controller.Api
open Fsharp.Controller.Api.Models
open Fsharp.Controller.Api.Routes.Routes
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore

/// <summary>
/// Defines the controller for managing teachers.
/// </summary>
type TeachersController(context: ApiContext) =
    inherit ControllerBase()
    
    /// <summary>
    /// Adds a new teacher.
    /// </summary>
    /// <param name="newTeacher">The teacher to add.</param>
    [<HttpPost>]
    [<Route(TeacherRoutes.Add)>]
    [<ProducesResponseType(typedefof<Teacher>, StatusCodes.Status201Created)>]
    member x.Add(
        [<FromBody>] newTeacher: Teacher) : Async<IActionResult> =
        async {
            context.Teachers.Add(newTeacher)
            |> ignore
            
            context.SaveChangesAsync()
            |> Async.AwaitTask
            |> ignore
            
            return x.CreatedAtAction(
                nameof x.Get,
                {| id = newTeacher.Id |},
                newTeacher)
        }
    
    /// <summary>
    /// Deletes a teacher.
    /// </summary>
    /// <param name="id">The id of the teacher.</param>
    [<HttpDelete(TeacherRoutes.Delete)>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member x.Delete(
        [<FromRoute>] id: Guid) : Async<IActionResult> =
        async {
            let! teacher =
                context.Teachers.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
            
            if obj.ReferenceEquals(teacher, null) then
                return x.NotFound()
            else
                context.Teachers.Remove(teacher)
                |> ignore
                
                context.SaveChangesAsync()
                |> Async.AwaitTask
                |> ignore
                
                return x.Ok()
        }
        
    /// <summary>
    /// Gets a teacher.
    /// </summary>
    /// <param name="id">The id of the teacher.</param>
    [<HttpGet(TeacherRoutes.Get)>]
    [<ProducesResponseType(typedefof<Teacher>, StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member x.Get(
        [<FromRoute>] id: Guid) : Async<IActionResult> =
        async {
            let! teacher =
                context.Teachers.FindAsync(id)
                    .AsTask()
                    |> Async.AwaitTask
                
            if obj.ReferenceEquals(teacher, null) then
                return x.NotFound()
            else
                return x.Ok(teacher)
        }
        
    /// <summary>
    /// Gets all teachers.
    /// </summary>
    [<HttpGet(TeacherRoutes.GetAll)>]
    [<ProducesResponseType(typedefof<List<Teacher>>, StatusCodes.Status200OK)>]
    member x.GetAll() : Async<IActionResult> =
        async {
            let! teachers =
                context.Teachers.ToListAsync()
                |> Async.AwaitTask
                
            return x.Ok(teachers)
        }
    
    /// <summary>
    /// Changes the properties of a teacher.
    /// </summary>
    /// <param name="id">The id of the teacher.</param>
    /// <param name="updatedTeacher">The updated teacher object.</param>
    [<HttpPut(TeacherRoutes.Update)>]
    [<ProducesResponseType(StatusCodes.Status200OK)>]
    [<ProducesResponseType(StatusCodes.Status400BadRequest)>]
    [<ProducesResponseType(StatusCodes.Status404NotFound)>]
    member x.Update(
        [<FromRoute>] id: Guid,
        [<FromBody>] updatedTeacher: Teacher) : Async<IActionResult> =
        async{
            if id <> updatedTeacher.Id then
                return x.BadRequest()
            else
                
                let! teacher =
                    context.Teachers.FindAsync(id)
                        .AsTask()
                        |> Async.AwaitTask
                
                if obj.ReferenceEquals(teacher, null) then
                    return x.NotFound()
                else
                    context.Teachers.Entry(teacher)
                        .CurrentValues.SetValues(updatedTeacher)
                    
                    context.SaveChangesAsync()
                    |> Async.AwaitTask
                    |> ignore
                    
                    return x.Ok()
        }
