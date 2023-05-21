using Csharp.Controller.Api.Models;
using Csharp.Controller.Api.Routes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Controller.Api.Controllers;

/// <summary>
/// Defines the controller for managing teachers.
/// </summary>
[ApiController]
[Route(TeacherRoutes.Base)]
public class TeachersController
    : ControllerBase
{
    private readonly ApiContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TeachersController"/> class.
    /// </summary>
    /// <param name="context">The db context.</param>
    public TeachersController(ApiContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new teacher.
    /// </summary>
    /// <param name="newTeacher">The teacher to add.</param>
    [HttpPost(TeacherRoutes.Add)]
    [ProducesResponseType(typeof(Teacher), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add(
        [FromBody] Teacher newTeacher)
    {
        _context.Teachers.Add(newTeacher);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Get),
            new { id = newTeacher.Id },
            newTeacher);
    }

    /// <summary>
    /// Deletes a teacher.
    /// </summary>
    /// <param name="id">The id of the teacher entity.</param>
    /// <returns></returns>
    [HttpDelete(TeacherRoutes.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id)
    {
        var teacher =
            await _context.Teachers.FindAsync(id);

        if (teacher is null)
        {
            return NotFound();
        }

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();

        return Ok();
    }
    
    /// <summary>
    /// Gets a teacher.
    /// </summary>
    /// <param name="id">The id of the teacher entity.</param>
    [HttpGet(TeacherRoutes.Get)]
    [ProducesResponseType(typeof(Teacher), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id)
    {
        var teacher = 
            await _context.Teachers.FindAsync(id);

        return teacher is null
            ? NotFound()
            : Ok(teacher);
    }

    /// <summary>
    /// Gets all teachers.
    /// </summary>
    [HttpGet(TeacherRoutes.GetAll)]
    [ProducesResponseType(typeof(List<Teacher>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var teachers = 
            await _context.Teachers
                .ToListAsync();

        return Ok(teachers);
    }

    /// <summary>
    /// Changes the properties of a teacher.
    /// </summary>
    /// <param name="id">The id of the teacher entity.</param>
    /// <param name="updatedTeacher">The teacher object with updated properties.</param>
    /// <returns></returns>
    [HttpPut(TeacherRoutes.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] Teacher updatedTeacher)
    {
        if (id != updatedTeacher.Id)
        {
            return BadRequest();
        }
        
        var teacher =
            await _context.Teachers.FindAsync(id);

        if (teacher is null)
        {
            return NotFound();
        }

        _context.Teachers.Entry(teacher)
            .CurrentValues.SetValues(updatedTeacher);

        await _context.SaveChangesAsync();
        return Ok();
    }
}