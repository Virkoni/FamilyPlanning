using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с календарными событиями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CalendarEventController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера календарных событий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public CalendarEventController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех календарных событий.
    /// </summary>
    /// <returns>Возвращает список всех календарных событий.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<CalendarEvent> calendarEvents = _context.CalendarEvents.ToList();
        return Ok(calendarEvents);
    }

    /// <summary>
    /// Получение календарного события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор календарного события.</param>
    /// <returns>Возвращает календарное событие с указанным идентификатором или сообщение об ошибке, если событие не найдено.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        CalendarEvent calendarEvent = _context.CalendarEvents.Find(id);

        if (calendarEvent == null)
        {
            return NotFound("CalendarEvent not found");
        }

        return Ok(calendarEvent);
    }

    /// <summary>
    /// Добавление нового календарного события.
    /// </summary>
    /// <param name="calendarEvent">Модель данных календарного события для добавления.</param>
    /// <returns>Возвращает созданное календарное событие и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(CalendarEvent calendarEvent)
    {
        _context.CalendarEvents.Add(calendarEvent);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = calendarEvent.Id }, calendarEvent);
    }

    /// <summary>
    /// Обновление календарного события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор календарного события для обновления.</param>
    /// <param name="calendarEvent">Модель данных для обновления календарного события.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии события.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, CalendarEvent calendarEvent)
    {
        if (id != calendarEvent.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(calendarEvent).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.CalendarEvents.Any(e => e.Id == id))
            {
                return NotFound("CalendarEvent not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление календарного события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор календарного события для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии события с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        CalendarEvent calendarEvent = _context.CalendarEvents.Find(id);

        if (calendarEvent == null)
        {
            return NotFound("CalendarEvent not found");
        }

        _context.CalendarEvents.Remove(calendarEvent);
        _context.SaveChanges();

        return Ok();
    }
}
