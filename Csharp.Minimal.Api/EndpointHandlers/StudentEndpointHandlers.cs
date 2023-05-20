using Csharp.Minimal.Api.Models;
using Csharp.Minimal.Api.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Minimal.Api.EndpointHandlers;

/// <summary>
/// Defines all student endpoint handlers.
/// </summary>
public static class StudentEndpointHandlers
{
    /// <summary>
    /// Adds a student.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="newStudent">The student to add.</param>
    /// <returns></returns>
    public static async Task<IResult> AddStudent(
        ApiContext context,
        [FromBody] Student newStudent)
    { 
        context.Students.Add(newStudent);
        await context.SaveChangesAsync();
                
        return Results.Created(
            StudentRoutes.Get
                .Replace(
                    "{id:guid}",
                    newStudent.Id.ToString("D")), 
            newStudent);
    }

    /// <summary>
    /// Deletes a student.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the student.</param>
    /// <returns></returns>
    public static async Task<IResult> DeleteStudent(
        ApiContext context,
        [FromRoute] Guid id)
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
    }

    /// <summary>
    /// Gets a single student by id.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the student.</param>
    /// <returns></returns>
    public static async Task<IResult> GetStudent(
        ApiContext context,
        [FromRoute] Guid id)
    {
        var student =
            await context.Students.FindAsync(id);

        return student is null
            ? Results.NotFound()
            : Results.Ok(student);
    }

    /// <summary>
    /// Gets all students.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <returns></returns>
    public static async Task<IResult> GetAllStudents(ApiContext context)
    {
        var students =
            await context.Students.ToListAsync();

        return Results.Ok(students);
    }

    /// <summary>
    /// Changes the properties of a student.
    /// </summary>
    /// <param name="context">The db context.</param>
    /// <param name="id">The id of the student.</param>
    /// <param name="updatedStudent">The student object with updated properties.</param>
    /// <returns></returns>
    public static async Task<IResult> UpdateStudent(
        ApiContext context,
        [FromRoute] Guid id,
        [FromBody] Student updatedStudent)
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

        context.Students.Entry(student)
            .CurrentValues.SetValues(updatedStudent);

        await context.SaveChangesAsync();
        return Results.Ok();
    }
}