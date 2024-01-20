using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly family_planningContext _context;

    public RolesController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Role> roles = _context.Roles.ToList();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Role role = _context.Roles.Find(id);

        if (role == null)
        {
            return NotFound("Role not found");
        }

        return Ok(role);
    }

    [HttpPost]
    public IActionResult Add(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Role role)
    {
        if (id != role.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(role).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Roles.Any(e => e.Id == id))
            {
                return NotFound("Role not found");
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
        Role role = _context.Roles.Find(id);

        if (role == null)
        {
            return NotFound("Role not found");
        }

        _context.Roles.Remove(role);
        _context.SaveChanges();

        return Ok();
    }
}
