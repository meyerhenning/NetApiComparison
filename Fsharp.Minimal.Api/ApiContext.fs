namespace Fsharp.Minimal.Api

open Fsharp.Minimal.Api.Models
open Microsoft.EntityFrameworkCore

/// <summary>
/// The db context representing all data used in the api.
/// </summary>
type ApiContext(options: DbContextOptions<ApiContext>) =
    inherit DbContext(options)
    
    [<DefaultValue>]
    val mutable private _students: DbSet<Student>
    
    [<DefaultValue>]
    val mutable private _teachers: DbSet<Teacher>
    
    /// <summary>
    /// Gets or sets the <see cref="DbSet{EntityT}"/> managing students.
    /// </summary>
    member x.Students
        with get() = x._students
        and set value = x._students <- value

    /// <summary>
    /// Gets or sets the <see cref="DbSet{EntityT}"/> managing teachers.
    /// </summary>
    member x.Teachers
        with get() = x._teachers
        and set value = x._teachers <- value