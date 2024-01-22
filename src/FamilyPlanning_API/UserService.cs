using System.Threading.Tasks;
using FamilyPlanning_API.Contracts.WebUser;
using FamilyPlanning_API.Models;
using Mapster;
using CustomTask = FamilyPlanning_API.Models.Task;

public interface IUserService
{
    Task<CustomTask> CreateAsync(CreateUserRequest request);
}

public class UserService : IUserService
{
    private readonly family_planningContext _context;

    public UserService(family_planningContext context)
    {
        _context = context;
    }

    public async Task<CustomTask> CreateAsync(CreateUserRequest request)
    {
        var userDto = request.Adapt<WebUser>();
        await _context.WebUsers.AddAsync(userDto);
        await _context.SaveChangesAsync();
        return userDto.Adapt<CustomTask>();
    }
}
