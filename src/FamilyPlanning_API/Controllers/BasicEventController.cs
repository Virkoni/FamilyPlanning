using FamilyPlanning_API.Contracts.BasicEvent;
using FamilyPlanning_API.Contracts.WebUser;
using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с базовыми событиями.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BasicEventController : ControllerBase
{
    private readonly family_planningContext _context;
    private readonly BasicEventService _eventService;

    /// <summary>
    /// Конструктор контроллера базовых событий.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public BasicEventController(family_planningContext context, BasicEventService eventService)
    {
        _context = context;
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    }


    /// <summary>
    /// Получение всех базовых событий.
    /// </summary>
    /// <returns>Возвращает список всех базовых событий.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<BasicEvent> events = _context.BasicEvents.ToList();
        return Ok(events);
    }

    /// <summary>
    /// Получение базового события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор базового события.</param>
    /// <returns>Возвращает базовое событие с указанным идентификатором или сообщение об ошибке, если событие не найдено.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BasicEvent basicEvent = _context.BasicEvents.Find(id);

        if (basicEvent == null)
        {
            return NotFound("Event not found");
        }

        return Ok(basicEvent);
    }

    /// <summary>
    /// Добавление нового базового события.
    /// </summary>
    /// <param name="basicEvent">Модель данных базового события для добавления.</param>
    /// <returns>Возвращает созданное базовое событие и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public async Task<IActionResult> Add(CreateBasicEventRequest request)
    {
        try
        {
            await _eventService.CreateAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest("Error creating a basic task: " + ex.Message);
        }
    }

    /// <summary>
    /// Обновление базового события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор базового события для обновления.</param>
    /// <param name="basicEvent">Модель данных для обновления базового события.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии события.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, BasicEvent basicEvent)
    {
        if (id != basicEvent.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(basicEvent).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.BasicEvents.Any(e => e.Id == id))
            {
                return NotFound("Event not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление базового события по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор базового события для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии события с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        BasicEvent basicEvent = _context.BasicEvents.Find(id);

        if (basicEvent == null)
        {
            return NotFound("Event not found");
        }

        _context.BasicEvents.Remove(basicEvent);
        _context.SaveChanges();

        return Ok();
    }
}
