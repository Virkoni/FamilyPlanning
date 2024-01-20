using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class FamilyEventController : ControllerBase
{
    private readonly family_planningContext _context;

    public FamilyEventController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyEvent> familyEvents = _context.FamilyEvents.ToList();
        return Ok(familyEvents);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyEvent familyEvent = _context.FamilyEvents.Find(id);

        if (familyEvent == null)
        {
            return NotFound("Family event not found");
        }

        return Ok(familyEvent);
    }

    [HttpPost]
    public IActionResult Add(FamilyEvent familyEvent)
    {
        _context.FamilyEvents.Add(familyEvent);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyEvent.Id }, familyEvent);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyEvent familyEvent)
    {
        if (id != familyEvent.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyEvent).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyEvents.Any(e => e.Id == id))
            {
                return NotFound("Family event not found");
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
        FamilyEvent familyEvent = _context.FamilyEvents.Find(id);

        if (familyEvent == null)
        {
            return NotFound("Family event not found");
        }

        _context.FamilyEvents.Remove(familyEvent);
        _context.SaveChanges();

        return Ok();
    }
}
