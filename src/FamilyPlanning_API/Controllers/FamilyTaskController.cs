using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с задачами семьи.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FamilyTaskController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера задач семьи.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public FamilyTaskController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех задач семьи.
    /// </summary>
    /// <returns>Возвращает список всех задач семьи.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyTask> familyTasks = _context.FamilyTasks.ToList();
        return Ok(familyTasks);
    }

    /// <summary>
    /// Получение задачи семьи по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи семьи.</param>
    /// <returns>Возвращает задачу семьи с указанным идентификатором или сообщение об ошибке, если задача не найдена.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyTask familyTask = _context.FamilyTasks.Find(id);

        if (familyTask == null)
        {
            return NotFound("Family task not found");
        }

        return Ok(familyTask);
    }

    /// <summary>
    /// Добавление новой задачи семьи.
    /// </summary>
    /// <param name="familyTask">Модель данных задачи семьи для добавления.</param>
    /// <returns>Возвращает созданную задачу семьи и её маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(FamilyTask familyTask)
    {
        _context.FamilyTasks.Add(familyTask);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyTask.Id }, familyTask);
    }

    /// <summary>
    /// Обновление задачи семьи по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи семьи для обновления.</param>
    /// <param name="familyTask">Модель данных для обновления задачи семьи.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии задачи.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyTask familyTask)
    {
        if (id != familyTask.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyTask).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyTasks.Any(e => e.Id == id))
            {
                return NotFound("Family task not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление задачи семьи по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи семьи для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии задачи с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        FamilyTask familyTask = _context.FamilyTasks.Find(id);

        if (familyTask == null)
        {
            return NotFound("Family task not found");
        }

        _context.FamilyTasks.Remove(familyTask);
        _context.SaveChanges();

        return Ok();
    }
}
