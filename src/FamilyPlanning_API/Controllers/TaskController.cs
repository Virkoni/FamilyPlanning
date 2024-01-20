using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CustomTask = FamilyPlanning_API.Models.Task; 

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly family_planningContext _context;

    public TaskController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<CustomTask> tasks = _context.Tasks.ToList();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        CustomTask task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound("Task not found");
        }

        return Ok(task);
    }

    [HttpPost]
    public IActionResult Add(CustomTask task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomTask task)
    {
        if (id != task.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == id))
            {
                return NotFound("Task not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        CustomTask task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound("Task not found");
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return Ok();
    }
}
