namespace Csharp.Minimal.Api.Routes;

/// <summary>
/// Defines the routes to the teacher endpoints.
/// </summary>
internal static class TeacherRoutes
{
    /// <summary>
    /// The route to the endpoint with the action:
    /// add teacher
    /// </summary>
    public const string Add = Base;
    
    /// <summary>
    /// The base route of all teacher endpoints.
    /// </summary>
    private const string Base = "/api/Teachers";
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// delete teacher
    /// </summary>
    public const string Delete = Get;
    
    /// <summary>
    /// The route to the endpoint with the action:
    /// get teacher
    /// </summary>
    public const string Get = $"{Base}/{{id:guid}}";

    /// <summary>
    /// The route to the endpoint with the action:
    /// get all teachers
    /// </summary>
    public const string GetAll = Base;

    /// <summary>
    /// The route to the endpoint with the action:
    /// update teachers
    /// </summary>
    public const string Update = Get;
}
