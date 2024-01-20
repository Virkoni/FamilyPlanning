using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class WebUserController : ControllerBase
{
    private readonly family_planningContext _context;

    public WebUserController(family_planningContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<WebUser> users = _context.WebUsers.ToList();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        WebUser user = _context.WebUsers.Find(id);

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Add(WebUser user)
    {
        _context.WebUsers.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, WebUser user)
    {
        if (id != user.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WebUsers.Any(e => e.Id == id))
            {
                return NotFound("User not found");
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
        WebUser user = _context.WebUsers.Find(id);

        if (user == null)
        {
            return NotFound("User not found");
        }

        _context.WebUsers.Remove(user);
        _context.SaveChanges();

        return Ok();
    }
}
