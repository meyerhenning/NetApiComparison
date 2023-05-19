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
    /// Adds a new student.
    /// </summary>
    /// <param name="newStudent">The student to add.</param>
    [HttpPost(StudentRoutes.Add)]
    [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
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
    /// Deletes a student.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    /// <returns></returns>
    [HttpDelete(StudentRoutes.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Gets a student.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    [HttpGet(StudentRoutes.Get)]
    [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// Gets all students.
    /// </summary>
    [HttpGet(StudentRoutes.GetAll)]
    [ProducesResponseType(typeof(List<Student>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var students = 
            await _context.Students
                .ToListAsync();

        return Ok(students);
    }

    /// <summary>
    /// Changes the properties of a student.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    /// <param name="updatedStudent">The student object with updated properties.</param>
    /// <returns></returns>
    [HttpPut(StudentRoutes.Update)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        _context.Students.Entry(student)
            .CurrentValues.SetValues(updatedStudent);

        await _context.SaveChangesAsync();
        return Ok();
    }
}