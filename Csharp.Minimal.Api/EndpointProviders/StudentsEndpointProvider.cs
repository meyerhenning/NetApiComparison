using Csharp.Minimal.Api.Models;
using Csharp.Minimal.Api.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Minimal.Api.EndpointProviders;

/// <summary>
/// Represents the provider of all students endpoints.
/// </summary>
public static class StudentsEndpointProvider
{
    /// <summary>
    /// Registers all students endpoints.
    /// </summary>
    /// <param name="app">The web application.</param>
    public static void RegisterEndpoints(WebApplication app)
    {
        // Gets all students
        app.MapGet(StudentsRoutes.GetAll,
            async (ApiContext context) 
                => await context.Students.ToListAsync());

        // Gets a single student by id
        app.MapGet(StudentsRoutes.Get,
            async (ApiContext context, [FromRoute] Guid id) =>
            {
                var student =
                    await context.Students.FindAsync(id);

                return student is null
                    ? Results.NotFound()
                    : Results.Ok(student);
            });

        // Adds a student
        app.MapPost(StudentsRoutes.Add,
            async (ApiContext context, [FromBody] Student newStudent) =>
            {
                context.Students.Add(newStudent);
                await context.SaveChangesAsync();
                
                return Results.Created(
                    $"/Students/{newStudent.Id}",
                    newStudent);
            });

        // Updates a student
        app.MapPut(StudentsRoutes.Update,
            async (ApiContext context, [FromRoute] Guid id, [FromBody] Student updatedStudent) =>
            {
                var student =
                    await context.Students.FindAsync(id);

                if (student is null)
                {
                    return Results.NotFound();
                }

                student.FirstName = updatedStudent.FirstName;
                student.LastName = updatedStudent.LastName;

                await context.SaveChangesAsync();
                return Results.Ok();
            });

        // Deletes a student
        app.MapDelete(StudentsRoutes.Delete,
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
            });
    }
}