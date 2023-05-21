namespace Csharp.Controller.Api.Models;

/// <summary>
/// Defines a teacher entity.
/// </summary>
public record Teacher
{
    /// <summary>
    /// Gets or sets the firstname.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the lastname.
    /// </summary>
    public string LastName { get; set; }
}