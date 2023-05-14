using Csharp.Minimal.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(
    o =>
        o.UseInMemoryDatabase("ApiDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Gets all students
app.MapGet("/Students",
    async (ApiContext context) 
        => await context.Students.ToListAsync());

// Gets a single student by id
app.MapGet("/Students/{id:guid}",
    async (ApiContext context, [FromRoute] Guid id) =>
    {
        var student =
            await context.Students.FindAsync(id);

        return student is null
            ? Results.NotFound()
            : Results.Ok(student);
    });

// Adds a student
app.MapPost("/Students",
    async (ApiContext context, [FromBody] Student newStudent) =>
    {
        context.Students.Add(newStudent);
        await context.SaveChangesAsync();
        
        return Results.Created(
            $"/Students/{newStudent.Id}",
            newStudent);
});

// Updates a student
app.MapPut("/Students/{id:guid}",
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
        return Results.NoContent();
    });

// Deletes a student
app.MapDelete("/Students/{id:guid}",
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

        return Results.Ok(student);
    });

app.Run();