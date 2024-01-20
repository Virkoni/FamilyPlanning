using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class FamilyMemberController : ControllerBase
{
    private readonly family_planningContext _context;

    public FamilyMemberController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyMember> familyMembers = _context.FamilyMembers.ToList();
        return Ok(familyMembers);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyMember familyMember = _context.FamilyMembers.Find(id);

        if (familyMember == null)
        {
            return NotFound("Family member not found");
        }

        return Ok(familyMember);
    }

    [HttpPost]
    public IActionResult Add(FamilyMember familyMember)
    {
        _context.FamilyMembers.Add(familyMember);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyMember.Id }, familyMember);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyMember familyMember)
    {
        if (id != familyMember.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyMember).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyMembers.Any(e => e.Id == id))
            {
                return NotFound("Family member not found");
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
        FamilyMember familyMember = _context.FamilyMembers.Find(id);

        if (familyMember == null)
        {
            return NotFound("Family member not found");
        }

        _context.FamilyMembers.Remove(familyMember);
        _context.SaveChanges();

        return Ok();
    }
}
