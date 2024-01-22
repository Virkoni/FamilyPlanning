using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с календарями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CalendarController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера календарей.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public CalendarController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех календарей.
    /// </summary>
    /// <returns>Возвращает список всех календарей.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Calendar> calendars = _context.Calendars.ToList();
        return Ok(calendars);
    }

    /// <summary>
    /// Получение календаря по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор календаря.</param>
    /// <returns>Возвращает календарь с указанным идентификатором или сообщение об ошибке, если календарь не найден.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Calendar calendar = _context.Calendars.Find(id);

        if (calendar == null)
        {
            return NotFound("Calendar not found");
        }

        return Ok(calendar);
    }

    /// <summary>
    /// Добавление нового календаря.
    /// </summary>
    /// <param name="calendar">Модель данных календаря для добавления.</param>
    /// <returns>Возвращает созданный календарь и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(Calendar calendar)
    {
        _context.Calendars.Add(calendar);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = calendar.Id }, calendar);
    }

    /// <summary>
    /// Обновление календаря по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор календаря для обновления.</param>
    /// <param name="calendar">Модель данных для обновления календаря.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии календаря.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Calendar calendar)
    {
        if (id != calendar.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(calendar).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Calendars.Any(e => e.Id == id))
            {
                return NotFound("Calendar not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление календаря по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор календаря для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии календаря с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Calendar calendar = _context.Calendars.Find(id);

        if (calendar == null)
        {
            return NotFound("Calendar not found");
        }

        _context.Calendars.Remove(calendar);
        _context.SaveChanges();

        return Ok();
    }
}
