using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с семьями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FamilyController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера семей.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public FamilyController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех семей.
    /// </summary>
    /// <returns>Возвращает список всех семей.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Family> families = _context.Families.ToList();
        return Ok(families);
    }

    /// <summary>
    /// Получение семьи по ее идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор семьи.</param>
    /// <returns>Возвращает семью с указанным идентификатором или сообщение об ошибке, если семья не найдена.</returns>
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

    /// <summary>
    /// Добавление новой семьи.
    /// </summary>
    /// <param name="family">Модель данных семьи для добавления.</param>
    /// <returns>Возвращает созданную семью и ее маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(Family family)
    {
        _context.Families.Add(family);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = family.Id }, family);
    }

    /// <summary>
    /// Обновление семьи по ее идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор семьи для обновления.</param>
    /// <param name="family">Модель данных для обновления семьи.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии семьи.</returns>
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

    /// <summary>
    /// Удаление семьи по ее идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор семьи для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии семьи с указанным идентификатором.</returns>
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
