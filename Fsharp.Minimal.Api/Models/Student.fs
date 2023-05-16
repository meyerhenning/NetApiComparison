namespace Fsharp.Minimal.Api.Models

open System

/// <summary>
/// Defines a student entity.
/// </summary>
[<CLIMutable>]
type Student = {
    FirstName: string
    Id: Guid
    LastName: string
}
