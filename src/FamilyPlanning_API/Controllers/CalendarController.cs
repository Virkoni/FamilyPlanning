using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class CalendarController : ControllerBase
{
    private readonly family_planningContext _context;

    public CalendarController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Calendar> calendars = _context.Calendars.ToList();
        return Ok(calendars);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Calendar calendar = _context.Calendars.Find(id);

        if (calendar == null)
        {
            return NotFound("Calendar not found");
        }

        return Ok(calendar);
    }

    [HttpPost]
    public IActionResult Add(Calendar calendar)
    {
        _context.Calendars.Add(calendar);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = calendar.Id }, calendar);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Calendar calendar)
    {
        if (id != calendar.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(calendar).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Calendars.Any(e => e.Id == id))
            {
                return NotFound("Calendar not found");
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
        Calendar calendar = _context.Calendars.Find(id);

        if (calendar == null)
        {
            return NotFound("Calendar not found");
        }

        _context.Calendars.Remove(calendar);
        _context.SaveChanges();

        return Ok();
    }
}
