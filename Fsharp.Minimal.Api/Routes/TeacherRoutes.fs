namespace Fsharp.Minimal.Api.Routes

[<RequireQualifiedAccess>]
module TeacherRoutes =

    /// <summary>
    /// The base route of all teacher endpoints.
    /// </summary>
    [<Literal>]
    let Base = "/api/Teachers"
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// add teacher
    /// </summary>
    [<Literal>]
    let Add = Base
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// get teacher
    /// </summary>
    [<Literal>]
    let Get = Base + "/{id:guid}"
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// get all teachers
    /// </summary>
    [<Literal>]
    let GetAll = Base
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// delete teacher
    /// </summary>
    [<Literal>]
    let Delete = Get
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// update teacher
    /// </summary>
    [<Literal>]
    let Update = Get
