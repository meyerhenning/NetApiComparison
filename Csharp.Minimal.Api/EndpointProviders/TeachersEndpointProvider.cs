using Csharp.Minimal.Api.EndpointHandlers;
using Csharp.Minimal.Api.Models;
using Csharp.Minimal.Api.Routes;

namespace Csharp.Minimal.Api.EndpointProviders;

/// <summary>
/// Represents the provider of all teacher endpoints.
/// </summary>
internal static class TeachersEndpointProvider
{
    private const string EndpointBaseTag = "Teachers";
    
    /// <summary>
    /// Registers all teacher endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    public static void RegisterEndpoints(WebApplication app)
    {
        app.MapPost(TeacherRoutes.Add, TeacherEndpointHandlers.AddTeacher)
            .Produces<Teacher>(StatusCodes.Status201Created)
            .WithTags(EndpointBaseTag);

        app.MapDelete(TeacherRoutes.Delete, TeacherEndpointHandlers.DeleteTeacher)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(EndpointBaseTag);
        
        app.MapGet(TeacherRoutes.Get, TeacherEndpointHandlers.GetTeacher)
            .Produces<Teacher>()
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(EndpointBaseTag);

        app.MapGet(TeacherRoutes.GetAll, TeacherEndpointHandlers.GetAllTeachers)
            .Produces<List<Teacher>>()
            .WithTags(EndpointBaseTag);

        app.MapPut(TeacherRoutes.Update, TeacherEndpointHandlers.UpdateTeacher)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags(EndpointBaseTag);
    }
}