using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class TableListController : ControllerBase
{
    private readonly family_planningContext _context;

    public TableListController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<TableList> tableLists = _context.TableLists.ToList();
        return Ok(tableLists);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        TableList tableList = _context.TableLists.Find(id);

        if (tableList == null)
        {
            return NotFound("TableList not found");
        }

        return Ok(tableList);
    }

    [HttpPost]
    public IActionResult Add(TableList tableList)
    {
        _context.TableLists.Add(tableList);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = tableList.Id }, tableList);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TableList tableList)
    {
        if (id != tableList.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(tableList).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TableLists.Any(e => e.Id == id))
            {
                return NotFound("TableList not found");
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
        TableList tableList = _context.TableLists.Find(id);

        if (tableList == null)
        {
            return NotFound("TableList not found");
        }

        _context.TableLists.Remove(tableList);
        _context.SaveChanges();

        return Ok();
    }
}
