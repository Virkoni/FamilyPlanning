using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с ролями пользователей.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly family_planningContext _context;

    /// <summary>
    /// Конструктор контроллера ролей пользователей.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public RolesController(family_planningContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Получение всех ролей пользователей.
    /// </summary>
    /// <returns>Возвращает список всех ролей пользователей.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Role> roles = _context.Roles.ToList();
        return Ok(roles);
    }

    /// <summary>
    /// Получение роли пользователя по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор роли пользователя.</param>
    /// <returns>Возвращает роль пользователя с указанным идентификатором или сообщение об ошибке, если роль не найдена.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        Role role = _context.Roles.Find(id);

        if (role == null)
        {
            return NotFound("Role not found");
        }

        return Ok(role);
    }

    /// <summary>
    /// Добавление новой роли пользователя.
    /// </summary>
    /// <param name="role">Модель данных роли пользователя для добавления.</param>
    /// <returns>Возвращает созданную роль пользователя и её маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public IActionResult Add(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    /// <summary>
    /// Обновление роли пользователя по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор роли пользователя для обновления.</param>
    /// <param name="role">Модель данных для обновления роли пользователя.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии роли.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Role role)
    {
        if (id != role.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(role).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Roles.Any(e => e.Id == id))
            {
                return NotFound("Role not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление роли пользователя по её идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор роли пользователя для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии роли с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Role role = _context.Roles.Find(id);

        if (role == null)
        {
            return NotFound("Role not found");
        }

        _context.Roles.Remove(role);
        _context.SaveChanges();

        return Ok();
    }
}
