using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ListItemController : ControllerBase
{
    private readonly family_planningContext _context;

    public ListItemController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<ListItem> listItems = _context.ListItems.ToList();
        return Ok(listItems);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        ListItem listItem = _context.ListItems.Find(id);

        if (listItem == null)
        {
            return NotFound("ListItem not found");
        }

        return Ok(listItem);
    }

    [HttpPost]
    public IActionResult Add(ListItem listItem)
    {
        _context.ListItems.Add(listItem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = listItem.Id }, listItem);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, ListItem listItem)
    {
        if (id != listItem.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(listItem).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ListItems.Any(e => e.Id == id))
            {
                return NotFound("ListItem not found");
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
        ListItem listItem = _context.ListItems.Find(id);

        if (listItem == null)
        {
            return NotFound("ListItem not found");
        }

        _context.ListItems.Remove(listItem);
        _context.SaveChanges();

        return Ok();
    }
}
