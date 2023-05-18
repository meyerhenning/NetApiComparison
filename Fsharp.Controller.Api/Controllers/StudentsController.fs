namespace Fsharp.Controller.Api.Controllers

open System
open Fsharp.Controller.Api
open Fsharp.Controller.Api.Models
open Fsharp.Controller.Api.Routes
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore

[<ApiController>]
[<Route(StudentRoutes.Base)>]
type StudentsController(context: ApiContext) =
    inherit ControllerBase()
    
    [<HttpPost>]
    [<Route(StudentRoutes.Add)>]
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
    
    [<HttpDelete(StudentRoutes.Delete)>]
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
        
    [<HttpGet(StudentRoutes.Get)>]
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
        
    [<HttpGet(StudentRoutes.GetAll)>]
    member x.GetAll() : Async<IActionResult> =
        async {
            let! students =
                context.Students.ToListAsync()
                |> Async.AwaitTask
                
            return x.Ok(students)
        }
    
    [<HttpPut(StudentRoutes.Update)>]
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