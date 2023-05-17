using Csharp.Controller.Api.Models;
using Csharp.Controller.Api.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Controller.Api.Controllers;

/// <summary>
/// Defines the controller for managing students.
/// </summary>
[ApiController]
[Route(StudentRoutes.Base)]
public class StudentsController
    : ControllerBase
{
    private readonly ApiContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentsController"/> class.
    /// </summary>
    /// <param name="context">The db context.</param>
    public StudentsController(ApiContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new student entity.
    /// </summary>
    /// <param name="newStudent">The student to add.</param>
    [HttpPost(StudentRoutes.Add)]
    public async Task<IActionResult> Add(
        [FromBody] Student newStudent)
    {
        _context.Students.Add(newStudent);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Get),
            new { id = newStudent.Id },
            newStudent);
    }

    /// <summary>
    /// Deletes a student entity.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    /// <returns></returns>
    [HttpDelete(StudentRoutes.Delete)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id)
    {
        var student =
            await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return Ok();
    }
    
    /// <summary>
    /// Gets a student entity.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    [HttpGet(StudentRoutes.Get)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id)
    {
        var student = 
            await _context.Students.FindAsync(id);

        return student is null
            ? NotFound()
            : Ok(student);
    }

    /// <summary>
    /// Gets all student entities.
    /// </summary>
    [HttpGet(StudentRoutes.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var students = 
            await _context.Students
                .ToListAsync();

        return Ok(students);
    }

    /// <summary>
    /// Changes properties on a student entity.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    /// <param name="updatedStudent">The student object with updated properties.</param>
    /// <returns></returns>
    [HttpPut(StudentRoutes.Update)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] Student updatedStudent)
    {
        if (id != updatedStudent.Id)
        {
            return BadRequest();
        }
        
        var student =
            await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        _context.Entry(updatedStudent).State =
            EntityState.Modified;

        await _context.SaveChangesAsync();
        return Ok();
    }
}