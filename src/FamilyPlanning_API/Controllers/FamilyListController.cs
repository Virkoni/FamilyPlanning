using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с списками семей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FamilyListController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера списков семей.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public FamilyListController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех списков семей.
    /// </summary>
    /// <returns>Возвращает список всех списков семей.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyList> familyLists = _context.FamilyLists.ToList();
        return Ok(familyLists);
    }

    /// <summary>
    /// Получение списка семей по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор списка семей.</param>
    /// <returns>Возвращает список семей с указанным идентификатором или сообщение об ошибке, если список не найден.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyList familyList = _context.FamilyLists.Find(id);

        if (familyList == null)
        {
            return NotFound("FamilyList not found");
        }

        return Ok(familyList);
    }

    /// <summary>
    /// Добавление нового списка семей.
    /// </summary>
    /// <param name="familyList">Модель данных списка семей для добавления.</param>
    /// <returns>Возвращает созданный список семей и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(FamilyList familyList)
    {
        _context.FamilyLists.Add(familyList);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyList.Id }, familyList);
    }

    /// <summary>
    /// Обновление списка семей по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор списка семей для обновления.</param>
    /// <param name="familyList">Модель данных для обновления списка семей.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии списка.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyList familyList)
    {
        if (id != familyList.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyList).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyLists.Any(e => e.Id == id))
            {
                return NotFound("FamilyList not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление списка семей по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор списка семей для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии списка с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        FamilyList familyList = _context.FamilyLists.Find(id);

        if (familyList == null)
        {
            return NotFound("FamilyList not found");
        }

        _context.FamilyLists.Remove(familyList);
        _context.SaveChanges();

        return Ok();
    }
}
