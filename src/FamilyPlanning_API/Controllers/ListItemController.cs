using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с элементами списка.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ListItemController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера элементов списка.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public ListItemController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех элементов списка.
    /// </summary>
    /// <returns>Возвращает список всех элементов списка.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<ListItem> listItems = _context.ListItems.ToList();
        return Ok(listItems);
    }

    /// <summary>
    /// Получение элемента списка по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор элемента списка.</param>
    /// <returns>Возвращает элемент списка с указанным идентификатором или сообщение об ошибке, если элемент не найден.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        ListItem listItem = _context.ListItems.Find(id);

        if (listItem == null)
        {
            return NotFound("ListItem not found");
        }

        return Ok(listItem);
    }

    /// <summary>
    /// Добавление нового элемента списка.
    /// </summary>
    /// <param name="listItem">Модель данных элемента списка для добавления.</param>
    /// <returns>Возвращает созданный элемент списка и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(ListItem listItem)
    {
        _context.ListItems.Add(listItem);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = listItem.Id }, listItem);
    }

    /// <summary>
    /// Обновление элемента списка по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор элемента списка для обновления.</param>
    /// <param name="listItem">Модель данных для обновления элемента списка.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии элемента списка.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, ListItem listItem)
    {
        if (id != listItem.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(listItem).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.ListItems.Any(e => e.Id == id))
            {
                return NotFound("ListItem not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление элемента списка по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор элемента списка для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии элемента списка с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        ListItem listItem = _context.ListItems.Find(id);

        if (listItem == null)
        {
            return NotFound("ListItem not found");
        }

        _context.ListItems.Remove(listItem);
        _context.SaveChanges();

        return Ok();
    }
}
