using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class CalendarEventController : ControllerBase
{
    private readonly family_planningContext _context;

    public CalendarEventController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<CalendarEvent> calendarEvents = _context.CalendarEvents.ToList();
        return Ok(calendarEvents);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        CalendarEvent calendarEvent = _context.CalendarEvents.Find(id);

        if (calendarEvent == null)
        {
            return NotFound("CalendarEvent not found");
        }

        return Ok(calendarEvent);
    }

    [HttpPost]
    public IActionResult Add(CalendarEvent calendarEvent)
    {
        _context.CalendarEvents.Add(calendarEvent);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = calendarEvent.Id }, calendarEvent);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CalendarEvent calendarEvent)
    {
        if (id != calendarEvent.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(calendarEvent).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CalendarEvents.Any(e => e.Id == id))
            {
                return NotFound("CalendarEvent not found");
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
        CalendarEvent calendarEvent = _context.CalendarEvents.Find(id);

        if (calendarEvent == null)
        {
            return NotFound("CalendarEvent not found");
        }

        _context.CalendarEvents.Remove(calendarEvent);
        _context.SaveChanges();

        return Ok();
    }
}
