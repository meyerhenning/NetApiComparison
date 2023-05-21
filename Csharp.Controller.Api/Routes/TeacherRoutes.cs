using Csharp.Controller.Api.Controllers;

namespace Csharp.Controller.Api.Routes;

/// <summary>
/// Defines the routes to the teacher endpoints.
/// </summary>
internal static class TeacherRoutes
{
    /// <summary>
    /// The route to <see cref="TeachersController.Add"/>.
    /// </summary>
    public const string Add = Base;
    
    /// <summary>
    /// The base route to access the endpoints
    /// of the <see cref="TeachersController"/>.
    /// </summary>
    public const string Base = "/api/Teachers";
    
    /// <summary>
    /// The route to <see cref="TeachersController.Delete"/>. 
    /// </summary>
    public const string Delete = Get;
    
    /// <summary>
    /// The route to <see cref="TeachersController.Get"/>.
    /// </summary>
    public const string Get = $"{Base}/{{id:guid}}";

    /// <summary>
    /// The route to <see cref="TeachersController.GetAll"/>.
    /// </summary>
    public const string GetAll = Base;

    /// <summary>
    /// The route to <see cref="TeachersController.Update"/>.
    /// </summary>
    public const string Update = Get;
}