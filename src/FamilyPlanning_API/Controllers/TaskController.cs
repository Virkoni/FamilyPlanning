using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using CustomTask = FamilyPlanning_API.Models.Task;

/// <summary>
/// Контроллер для работы с задачами.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера задач.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public TaskController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех задач.
    /// </summary>
    /// <returns>Возвращает список всех задач.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<CustomTask> tasks = _context.Tasks.ToList();
        return Ok(tasks);
    }

    /// <summary>
    /// Получение задачи по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи.</param>
    /// <returns>Возвращает задачу с указанным идентификатором или сообщение об ошибке, если задача не найдена.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        CustomTask task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound("Task not found");
        }

        return Ok(task);
    }

    /// <summary>
    /// Добавление новой задачи.
    /// </summary>
    /// <param name="task">Модель данных задачи для добавления.</param>
    /// <returns>Возвращает созданную задачу и её маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(CustomTask task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    /// <summary>
    /// Обновление задачи по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи для обновления.</param>
    /// <param name="task">Модель данных для обновления задачи.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии задачи.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomTask task)
    {
        if (id != task.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tasks.Any(e => e.Id == id))
            {
                return NotFound("Task not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление задачи по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии задачи с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        CustomTask task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound("Task not found");
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return Ok();
    }
}
