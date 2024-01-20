using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class BasicEventController : ControllerBase
{
    private readonly family_planningContext _context;

    public BasicEventController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<BasicEvent> events = _context.BasicEvents.ToList();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BasicEvent basicEvent = _context.BasicEvents.Find(id);

        if (basicEvent == null)
        {
            return NotFound("Event not found");
        }

        return Ok(basicEvent);
    }

    [HttpPost]
    public IActionResult Add(BasicEvent basicEvent)
    {
        _context.BasicEvents.Add(basicEvent);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = basicEvent.Id }, basicEvent);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, BasicEvent basicEvent)
    {
        if (id != basicEvent.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(basicEvent).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.BasicEvents.Any(e => e.Id == id))
            {
                return NotFound("Event not found");
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
        BasicEvent basicEvent = _context.BasicEvents.Find(id);

        if (basicEvent == null)
        {
            return NotFound("Event not found");
        }

        _context.BasicEvents.Remove(basicEvent);
        _context.SaveChanges();

        return Ok();
    }
}
