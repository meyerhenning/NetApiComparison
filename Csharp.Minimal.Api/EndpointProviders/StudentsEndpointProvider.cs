using Csharp.Minimal.Api.Models;
using Csharp.Minimal.Api.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Minimal.Api.EndpointProviders;

/// <summary>
/// Represents the provider of all student endpoints.
/// </summary>
public static class StudentsEndpointProvider
{
    private const string EndpointBaseTag = "Students";
    
    /// <summary>
    /// Registers all student endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    public static void RegisterEndpoints(WebApplication app)
    {
        // Adds a student
        app.MapPost(StudentRoutes.Add,
            async (ApiContext context, [FromBody] Student newStudent) =>
            {
                context.Students.Add(newStudent);
                await context.SaveChangesAsync();
                
                return Results.Created(
                    StudentRoutes.Get
                        .Replace(
                            "{id:guid}",
                            newStudent.Id.ToString("D")),
                    newStudent);
            })
            .WithTags(EndpointBaseTag);

        // Deletes a student
        app.MapDelete(StudentRoutes.Delete,
            async (ApiContext context, [FromRoute] Guid id) =>
            {
                var student =
                    await context.Students.FindAsync(id);

                if (student is null)
                {
                    return Results.NotFound();
                }

                context.Students.Remove(student);
                await context.SaveChangesAsync();

                return Results.Ok();
            })
            .WithTags(EndpointBaseTag);
        
        // Gets a single student by id
        app.MapGet(StudentRoutes.Get,
            async (ApiContext context, [FromRoute] Guid id) =>
            {
                var student =
                    await context.Students.FindAsync(id);

                return student is null
                    ? Results.NotFound()
                    : Results.Ok(student);
            })
            .WithTags(EndpointBaseTag);

        // Gets all students
        app.MapGet(StudentRoutes.GetAll,
            async (ApiContext context) 
                => await context.Students.ToListAsync())
            .WithTags(EndpointBaseTag);

        // Updates a student
        app.MapPut(StudentRoutes.Update,
            async (ApiContext context, [FromRoute] Guid id, [FromBody] Student updatedStudent) =>
            {
                if (id != updatedStudent.Id)
                {
                    return Results.BadRequest();
                }
                
                var student =
                    await context.Students.FindAsync(id);

                if (student is null)
                {
                    return Results.NotFound();
                }

                context.Entry(updatedStudent).State =
                    EntityState.Modified;

                await context.SaveChangesAsync();
                return Results.Ok();
            })
            .WithTags(EndpointBaseTag);
    }
}