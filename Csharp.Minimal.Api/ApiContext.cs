using Microsoft.EntityFrameworkCore;

namespace Csharp.Minimal.Api;

/// <summary>
/// The db context representing all data used in the api.
/// </summary>
public class ApiContext
    : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiContext"/> class.
    /// </summary>
    /// <param name="options">The db context options.</param>
    public ApiContext(DbContextOptions options)
        : base(options) { }
    
    /// <summary>
    /// Gets or sets the <see cref="DbSet{TEntity}"/> managing students.
    /// </summary>
    public DbSet<Student> Students { get; set; }
}
