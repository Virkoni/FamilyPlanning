using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с членами семьи.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class FamilyMemberController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера членов семьи.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public FamilyMemberController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех членов семьи.
    /// </summary>
    /// <returns>Возвращает список всех членов семьи.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<FamilyMember> familyMembers = _context.FamilyMembers.ToList();
        return Ok(familyMembers);
    }

    /// <summary>
    /// Получение члена семьи по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор члена семьи.</param>
    /// <returns>Возвращает члена семьи с указанным идентификатором или сообщение об ошибке, если член не найден.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        FamilyMember familyMember = _context.FamilyMembers.Find(id);

        if (familyMember == null)
        {
            return NotFound("Family member not found");
        }

        return Ok(familyMember);
    }

    /// <summary>
    /// Добавление нового члена семьи.
    /// </summary>
    /// <param name="familyMember">Модель данных члена семьи для добавления.</param>
    /// <returns>Возвращает созданного члена семьи и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(FamilyMember familyMember)
    {
        _context.FamilyMembers.Add(familyMember);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = familyMember.Id }, familyMember);
    }

    /// <summary>
    /// Обновление члена семьи по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор члена семьи для обновления.</param>
    /// <param name="familyMember">Модель данных для обновления члена семьи.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии члена.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, FamilyMember familyMember)
    {
        if (id != familyMember.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(familyMember).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FamilyMembers.Any(e => e.Id == id))
            {
                return NotFound("Family member not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление члена семьи по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор члена семьи для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии члена с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        FamilyMember familyMember = _context.FamilyMembers.Find(id);

        if (familyMember == null)
        {
            return NotFound("Family member not found");
        }

        _context.FamilyMembers.Remove(familyMember);
        _context.SaveChanges();

        return Ok();
    }
}
