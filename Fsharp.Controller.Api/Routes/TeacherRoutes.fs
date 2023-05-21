module Fsharp.Controller.Api.Routes.Routes

/// <summary>
/// Defines the routes to the teacher endpoints.
/// </summary>
[<RequireQualifiedAccess>]
module TeacherRoutes =

    /// <summary>
    /// The base route to access the endpoints
    /// of the TeachersController.
    /// </summary>
    [<Literal>]
    let Base = "/api/Teachers"
    
    /// <summary>
    /// The route to TeachersController.Add.
    /// </summary>
    [<Literal>]
    let Add = Base
    
    /// <summary>
    /// The route to TeachersController.Get.
    /// </summary>
    [<Literal>]
    let Get = Base + "/{id:guid}"
    
    /// <summary>
    /// The route to TeachersController.GetAll.
    /// </summary>
    [<Literal>]
    let GetAll = Base
    
    /// <summary>
    /// The route to TeachersController.Delete.
    /// </summary>
    [<Literal>]
    let Delete = Get
    
    /// <summary>
    /// The route to TeachersController.Update.
    /// </summary>
    [<Literal>]
    let Update = Get
