namespace Fsharp.Controller.Api.Models

open System

/// <summary>
/// Defines a teacher entity.
/// </summary>
[<CLIMutable>]
type Teacher = {
    FirstName: string
    Id: Guid
    LastName: string
}
