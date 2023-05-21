using Csharp.Minimal.Api.EndpointHandlers;
using Csharp.Minimal.Api.Models;
using Csharp.Minimal.Api.Routes;

namespace Csharp.Minimal.Api.EndpointProviders;

/// <summary>
/// Represents the provider of all student endpoints.
/// </summary>
internal static class StudentsEndpointProvider
{
    private const string EndpointBaseTag = "Students";
    
    /// <summary>
    /// Registers all student endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapPost(StudentRoutes.Add, StudentEndpointHandlers.AddStudent)
            .Produces<Student>(StatusCodes.Status201Created)
            .WithTags(EndpointBaseTag);

        app.MapDelete(StudentRoutes.Delete, StudentEndpointHandlers.DeleteStudent)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(EndpointBaseTag);
        
        app.MapGet(StudentRoutes.Get, StudentEndpointHandlers.GetStudent)
            .Produces<Student>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(EndpointBaseTag);

        app.MapGet(StudentRoutes.GetAll, StudentEndpointHandlers.GetAllStudents)
            .Produces<List<Student>>()
            .WithTags(EndpointBaseTag);

        app.MapPut(StudentRoutes.Update, StudentEndpointHandlers.UpdateStudent)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(EndpointBaseTag);
    }
}