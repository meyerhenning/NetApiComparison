namespace Fsharp.Minimal.Api.Routes

[<RequireQualifiedAccess>]
module StudentRoutes =

    /// <summary>
    /// The base route of all student endpoints.
    /// </summary>
    [<Literal>]
    let Base = "/api/Students"
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// add student
    /// </summary>
    [<Literal>]
    let Add = Base
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// get student
    /// </summary>
    [<Literal>]
    let Get = Base + "/{id:guid}"
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// get all students
    /// </summary>
    [<Literal>]
    let GetAll = Base
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// delete student
    /// </summary>
    [<Literal>]
    let Delete = Get
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// update student
    /// </summary>
    [<Literal>]
    let Update = Get

