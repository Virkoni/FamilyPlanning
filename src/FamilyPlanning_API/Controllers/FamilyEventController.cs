using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с семейными событиями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FamilyEventController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера семейных событий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public FamilyEventController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех семейных событий.
    /// </summary>
    /// <returns>Возвращает список всех семейных событий.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyEvent> familyEvents = _context.FamilyEvents.ToList();
        return Ok(familyEvents);
    }

    /// <summary>
    /// Получение семейного события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор семейного события.</param>
    /// <returns>Возвращает семейное событие с указанным идентификатором или сообщение об ошибке, если событие не найдено.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyEvent familyEvent = _context.FamilyEvents.Find(id);

        if (familyEvent == null)
        {
            return NotFound("Family event not found");
        }

        return Ok(familyEvent);
    }

    /// <summary>
    /// Добавление нового семейного события.
    /// </summary>
    /// <param name="familyEvent">Модель данных семейного события для добавления.</param>
    /// <returns>Возвращает созданное семейное событие и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(FamilyEvent familyEvent)
    {
        _context.FamilyEvents.Add(familyEvent);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyEvent.Id }, familyEvent);
    }

    /// <summary>
    /// Обновление семейного события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор семейного события для обновления.</param>
    /// <param name="familyEvent">Модель данных для обновления семейного события.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии события.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyEvent familyEvent)
    {
        if (id != familyEvent.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyEvent).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyEvents.Any(e => e.Id == id))
            {
                return NotFound("Family event not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление семейного события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор семейного события для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии события с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        FamilyEvent familyEvent = _context.FamilyEvents.Find(id);

        if (familyEvent == null)
        {
            return NotFound("Family event not found");
        }

        _context.FamilyEvents.Remove(familyEvent);
        _context.SaveChanges();

        return Ok();
    }
}
