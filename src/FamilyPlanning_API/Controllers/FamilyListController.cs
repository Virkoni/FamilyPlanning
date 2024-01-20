using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class FamilyListController : ControllerBase
{
    private readonly family_planningContext _context;

    public FamilyListController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyList> familyLists = _context.FamilyLists.ToList();
        return Ok(familyLists);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyList familyList = _context.FamilyLists.Find(id);

        if (familyList == null)
        {
            return NotFound("FamilyList not found");
        }

        return Ok(familyList);
    }

    [HttpPost]
    public IActionResult Add(FamilyList familyList)
    {
        _context.FamilyLists.Add(familyList);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyList.Id }, familyList);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyList familyList)
    {
        if (id != familyList.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyList).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyLists.Any(e => e.Id == id))
            {
                return NotFound("FamilyList not found");
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
        FamilyList familyList = _context.FamilyLists.Find(id);

        if (familyList == null)
        {
            return NotFound("FamilyList not found");
        }

        _context.FamilyLists.Remove(familyList);
        _context.SaveChanges();

        return Ok();
    }
}
