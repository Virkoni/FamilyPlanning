using FamilyPlanning_API.Contracts.WebUser;
using FamilyPlanning_API.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Контроллер для работы с пользователями веб-приложения.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WebUserController : ControllerBase
{
    private readonly family_planningContext _context;
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор контроллера пользователей веб-приложения.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    public WebUserController(family_planningContext context)
    {
        _context = context;

    }

    /// <summary>
    /// Получение всех пользователей веб-приложения.
    /// </summary>
    /// <returns>Возвращает список всех пользователей.</returns>
    [HttpGet]
    public IActionResult GetAll()
    {
        List<WebUser> users = _context.WebUsers.ToList();
        return Ok(users);
    }

    /// <summary>
    /// Получение пользователя веб-приложения по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Возвращает пользователя с указанным идентификатором или сообщение об ошибке, если пользователь не найден.</returns>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        WebUser user = _context.WebUsers.Find(id);

        if (user == null)
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }

    /// <summary>
    /// Добавление нового пользователя веб-приложения.
    /// </summary>
    /// <param name="user">Модель данных пользователя для добавления.</param>
    /// <returns>Возвращает созданного пользователя и его маршрут или сообщение об ошибке при неверных входных данных.</returns>
    [HttpPost]
    public async Task<IActionResult> Add(CreateUserRequest request)
    {
        try
        {
            await _userService.CreateAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return BadRequest("Error creating user: " + ex.Message);
        }
    }
    /// <summary>
    /// Обновление пользователя веб-приложения по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя для обновления.</param>
    /// <param name="user">Модель данных для обновления пользователя.</param>
    /// <returns>Возвращает успешное сообщение о выполнении обновления или сообщение об ошибке при неверных входных данных или отсутствии пользователя.</returns>
    [HttpPut("{id}")]
    public IActionResult Update(int id, WebUser user)
    {
        if (id != user.Id)
        {
            return BadRequest("Invalid ID");
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WebUsers.Any(e => e.Id == id))
            {
                return NotFound("User not found");
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление пользователя веб-приложения по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя для удаления.</param>
    /// <returns>Возвращает успешное сообщение о выполнении удаления или сообщение об ошибке при отсутствии пользователя с указанным идентификатором.</returns>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        WebUser user = _context.WebUsers.Find(id);

        if (user == null)
        {
            return NotFound("User not found");
        }

        _context.WebUsers.Remove(user);
        _context.SaveChanges();

        return Ok();
    }
}
