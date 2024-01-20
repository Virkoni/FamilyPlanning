using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class FamilyController : ControllerBase
{
    private readonly family_planningContext _context;

    public FamilyController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Family> families = _context.Families.ToList();
        return Ok(families);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Family family = _context.Families.Find(id);

        if (family == null)
        {
            return NotFound("Family not found");
        }

        return Ok(family);
    }

    [HttpPost]
    public IActionResult Add(Family family)
    {
        _context.Families.Add(family);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = family.Id }, family);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Family family)
    {
        if (id != family.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(family).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Families.Any(e => e.Id == id))
            {
                return NotFound("Family not found");
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
        Family family = _context.Families.Find(id);

        if (family == null)
        {
            return NotFound("Family not found");
        }

        _context.Families.Remove(family);
        _context.SaveChanges();

        return Ok();
    }
}
