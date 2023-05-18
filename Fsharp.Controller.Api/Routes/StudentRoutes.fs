namespace Fsharp.Controller.Api.Routes

[<RequireQualifiedAccess>]
module StudentRoutes =

    [<Literal>]
    let Base = "/api/Students"
    
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