namespace Fsharp.Controller.Api.Controllers

open System
open Fsharp.Controller.Api
open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("[controller]")>]
type StudentsController(context: ApiContext) =
    inherit ControllerBase()
    
    // TODO: Add async endpoints
    
    [<HttpGet("{id:guid}")>]
    member x.Get(
        [<FromRoute>] id: Guid) : ActionResult =
        
        let student =
            context.Find(id)
        
        if obj.ReferenceEquals(student, null) then
            base.NotFound()
        else
            base.Ok(student)