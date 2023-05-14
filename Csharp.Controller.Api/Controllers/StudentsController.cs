using Csharp.Controller.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Controller.Api.Controllers;

/// <summary>
/// Defines the controller for managing students.
/// </summary>
[ApiController]
[Route("[controller]")]
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
    [HttpPost]
    public async Task<ActionResult<Student>> Add(
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
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Student>> Delete(
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

        return Ok(student);
    }
    
    /// <summary>
    /// Gets a student entity.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(
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
    [HttpGet]
    public ActionResult GetAll()
    {
        var students = 
            _context.Students.AsNoTracking();

        return Ok(students);
    }

    /// <summary>
    /// Changes properties on a student entity.
    /// </summary>
    /// <param name="id">The id of the student entity.</param>
    /// <param name="updatedStudent">The student object with updated properties.</param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] Student updatedStudent)
    {
        var student =
            await _context.Students.FindAsync(id);

        if (student is null)
        {
            return NotFound();
        }

        student.FirstName = updatedStudent.FirstName;
        student.LastName = updatedStudent.LastName;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}