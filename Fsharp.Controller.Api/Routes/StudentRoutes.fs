namespace Fsharp.Controller.Api.Routes

/// <summary>
/// Defines the routes to the student endpoints.
/// </summary>
[<RequireQualifiedAccess>]
module StudentRoutes =

    /// <summary>
    /// The base route to access the endpoints
    /// of the StudentsController.
    /// </summary>
    [<Literal>]
    let Base = "/api/Students"
    
    /// <summary>
    /// The route to StudentsController.Add.
    /// </summary>
    [<Literal>]
    let Add = Base
    
    /// <summary>
    /// The route to StudentsController.Get.
    /// </summary>
    [<Literal>]
    let Get = Base + "/{id:guid}"
    
    /// <summary>
    /// The route to StudentsController.GetAll.
    /// </summary>
    [<Literal>]
    let GetAll = Base
    
    /// <summary>
    /// The route to StudentsController.Delete.
    /// </summary>
    [<Literal>]
    let Delete = Get
    
    /// <summary>
    /// The route to StudentsController.Update.
    /// </summary>
    [<Literal>]
    let Update = Get