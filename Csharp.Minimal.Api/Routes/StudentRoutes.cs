namespace Csharp.Minimal.Api.Routes;

/// <summary>
/// Defines the routes to the student endpoints.
/// </summary>
internal static class StudentRoutes
{
    /// <summary>
    /// The route to the endpoint with the action:
    /// add student
    /// </summary>
    public const string Add = Base;
    
    /// <summary>
    /// The base route of all student endpoints.
    /// </summary>
    private const string Base = "/api/Students";
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// delete student
    /// </summary>
    public const string Delete = Get;
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// get student
    /// </summary>
    public const string Get = $"{Base}/{{id:guid}}";

    /// <summary>
    /// The route to the endpoint with the action:
    /// get all students
    /// </summary>
    public const string GetAll = Base;

    /// <summary>
    /// The route to the endpoint with the action:
    /// update student
    /// </summary>
    public const string Update = Get;
}
