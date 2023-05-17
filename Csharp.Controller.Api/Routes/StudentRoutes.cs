using Csharp.Controller.Api.Controllers;

namespace Csharp.Controller.Api.Routes;

/// <summary>
/// Defines the routes to the student endpoints.
/// </summary>
internal static class StudentRoutes
{
    /// <summary>
    /// The route to <see cref="StudentsController.Add"/>.
    /// </summary>
    public const string Add = Base;
    
    /// <summary>
    /// The base route to access the endpoints
    /// of the <see cref="StudentsController"/>.
    /// </summary>
    public const string Base = "/api/Students";
    
    /// <summary>
    /// The route to <see cref="StudentsController.Delete"/>. 
    /// </summary>
    public const string Delete = Get;
    
    /// <summary>
    /// The route to <see cref="StudentsController.Get"/>.
    /// </summary>
    public const string Get = $"{Base}/{{id:guid}}";

    /// <summary>
    /// The route to <see cref="StudentsController.GetAll"/>.
    /// </summary>
    public const string GetAll = Base;

    /// <summary>
    /// The route to <see cref="StudentsController.Update"/>.
    /// </summary>
    public const string Update = Get;
}