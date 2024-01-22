using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с таблицами списков.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TableListController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера таблиц списков.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public TableListController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех таблиц списков.
    /// </summary>
    /// <returns>Возвращает список всех таблиц списков.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<TableList> tableLists = _context.TableLists.ToList();
        return Ok(tableLists);
    }

    /// <summary>
    /// Получение таблицы списка по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор таблицы списка.</param>
    /// <returns>Возвращает таблицу списка с указанным идентификатором или сообщение об ошибке, если таблица не найдена.</returns>
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

    /// <summary>
    /// Добавление новой таблицы списка.
    /// </summary>
    /// <param name="tableList">Модель данных таблицы списка для добавления.</param>
    /// <returns>Возвращает созданную таблицу списка и её маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(TableList tableList)
    {
        _context.TableLists.Add(tableList);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = tableList.Id }, tableList);
    }

    /// <summary>
    /// Обновление таблицы списка по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор таблицы списка для обновления.</param>
    /// <param name="tableList">Модель данных для обновления таблицы списка.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии таблицы.</returns>
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

    /// <summary>
    /// Удаление таблицы списка по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор таблицы списка для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии таблицы с указанным идентификатором.</returns>
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
