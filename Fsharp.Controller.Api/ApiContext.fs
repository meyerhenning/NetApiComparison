namespace Fsharp.Controller.Api

open Fsharp.Controller.Api.Models
open Microsoft.EntityFrameworkCore

/// <summary>
/// The db context representing all data used in the api.
/// </summary>
type ApiContext(options: DbContextOptions<ApiContext>) =
    inherit DbContext(options)
    
    [<DefaultValue>]
    val mutable private _students: DbSet<Student>
    
    /// <summary>
    /// Gets or sets the <see cref="DbSet{EntityT}"/> managing students.
    /// </summary>
    member x.Students
        with get() = x._students
        and set value = x._students <- value