using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class FamilyTaskController : ControllerBase
{
    private readonly family_planningContext _context;

    public FamilyTaskController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyTask> familyTasks = _context.FamilyTasks.ToList();
        return Ok(familyTasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyTask familyTask = _context.FamilyTasks.Find(id);

        if (familyTask == null)
        {
            return NotFound("Family task not found");
        }

        return Ok(familyTask);
    }

    [HttpPost]
    public IActionResult Add(FamilyTask familyTask)
    {
        _context.FamilyTasks.Add(familyTask);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyTask.Id }, familyTask);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyTask familyTask)
    {
        if (id != familyTask.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyTask).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyTasks.Any(e => e.Id == id))
            {
                return NotFound("Family task not found");
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
        FamilyTask familyTask = _context.FamilyTasks.Find(id);

        if (familyTask == null)
        {
            return NotFound("Family task not found");
        }

        _context.FamilyTasks.Remove(familyTask);
        _context.SaveChanges();

        return Ok();
    }
}
