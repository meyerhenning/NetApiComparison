using Csharp.Minimal.Api.Models;
using Csharp.Minimal.Api.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Minimal.Api.EndpointHandlers;

/// <summary>
/// Defines all teacher endpoint handlers.
/// </summary>
public static class TeacherEndpointHandlers
{
    /// <summary>
    /// Adds a teacher.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="newTeacher">The teacher to add.</param>
    /// <returns></returns>
    public static async Task<IResult> AddTeacher(
        ApiContext context,
        [FromBody] Teacher newTeacher)
    { 
        context.Teachers.Add(newTeacher);
        await context.SaveChangesAsync();
                
        return Results.Created(
            TeacherRoutes.Get
                .Replace(
                    "{id:guid}",
                    newTeacher.Id.ToString("D")), 
            newTeacher);
    }

    /// <summary>
    /// Deletes a teacher.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the teacher.</param>
    /// <returns></returns>
    public static async Task<IResult> DeleteTeacher(
        ApiContext context,
        [FromRoute] Guid id)
    {
        var teacher =
            await context.Teachers.FindAsync(id);

        if (teacher is null)
        {
            return Results.NotFound();
        }

        context.Teachers.Remove(teacher);
        await context.SaveChangesAsync();

        return Results.Ok();
    }

    /// <summary>
    /// Gets a single teacher by id.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the teacher.</param>
    /// <returns></returns>
    public static async Task<IResult> GetTeacher(
        ApiContext context,
        [FromRoute] Guid id)
    {
        var teacher =
            await context.Teachers.FindAsync(id);

        return teacher is null
            ? Results.NotFound()
            : Results.Ok(teacher);
    }

    /// <summary>
    /// Gets all teachers.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <returns></returns>
    public static async Task<IResult> GetAllTeachers(ApiContext context)
    {
        var teachers =
            await context.Teachers.ToListAsync();

        return Results.Ok(teachers);
    }

    /// <summary>
    /// Changes the properties of a teacher.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the teacher.</param>
    /// <param name="updatedTeacher">The teacher object with updated properties.</param>
    /// <returns></returns>
    public static async Task<IResult> UpdateTeacher(
        ApiContext context,
        [FromRoute] Guid id,
        [FromBody] Teacher updatedTeacher)
    {
        if (id != updatedTeacher.Id)
        {
            return Results.BadRequest();
        }
        
        var teacher =
            await context.Teachers.FindAsync(id);

        if (teacher is null)
        {
            return Results.NotFound();
        }

        context.Teachers.Entry(teacher)
            .CurrentValues.SetValues(updatedTeacher);

        await context.SaveChangesAsync();
        return Results.Ok();
    }
}