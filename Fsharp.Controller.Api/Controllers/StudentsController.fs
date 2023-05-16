namespace Fsharp.Controller.Api.Controllers

open System
open Fsharp.Controller.Api
open Fsharp.Controller.Api.Models
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore

[<ApiController>]
[<Route("[controller]")>]
type StudentsController(context: ApiContext) =
    inherit ControllerBase()
    
    [<HttpPost>]
    member x.Add(
        [<FromBody>] newStudent: Student) : Async<ActionResult> =
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
    
    [<HttpDelete("{id:guid}")>]
    member x.Delete(
        [<FromRoute>] id: Guid) : Async<ActionResult> =
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
        
    [<HttpGet("{id:guid}")>]
    member x.Get(
        [<FromRoute>] id: Guid) : Async<ActionResult> =
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
        
    [<HttpGet>]
    member x.GetAll() : Async<ActionResult> =
        async {
            let! students =
                context.Students.ToListAsync()
                |> Async.AwaitTask
                
            return x.Ok(students)
        }
    
    [<HttpPut("{id:guid}")>]
    member x.Update(
        [<FromRoute>] id: Guid,
        [<FromBody>] updatedStudent: Student) : Async<ActionResult> =
        async{
            if id <> updatedStudent.Id then
                return x.BadRequest()
            else
                context.Entry(updatedStudent).State <-
                    EntityState.Modified
                
                context.SaveChangesAsync()
                |> Async.AwaitTask
                |> ignore
                
                return x.Ok()
        }