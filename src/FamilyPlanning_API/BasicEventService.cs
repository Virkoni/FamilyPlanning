using System.Threading.Tasks;
using FamilyPlanning_API.Contracts.BasicEvent;
using FamilyPlanning_API.Models;
using Mapster;
using CustomTask = FamilyPlanning_API.Models.Task;

public interface IBasicEventService
{
    Task<CustomTask> CreateAsync(CreateBasicEventRequest request);
}

public class BasicEventService : IBasicEventService
{
    private readonly family_planningContext _context;

    public BasicEventService(family_planningContext context)
    {
        _context = context;
    }

    public async Task<CustomTask> CreateAsync(CreateBasicEventRequest request)
    {
        var eventDto = request.Adapt<BasicEvent>();
        await _context.BasicEvents.AddAsync(eventDto);  
        await _context.SaveChangesAsync();
        return eventDto.Adapt<CustomTask>();
    }
}
