module Fsharp.Controller.Api.Routes.Routes

[<RequireQualifiedAccess>]
module TeacherRoutes =

    [<Literal>]
    let Base = "/api/Teachers"
    
    [<Literal>]
    let Add = Base
    
    [<Literal>]
    let Get = Base + "/{id:guid}"
    
    [<Literal>]
    let GetAll = Base
    
    [<Literal>]
    let Delete = Get
    
    [<Literal>]
    let Update = Get
