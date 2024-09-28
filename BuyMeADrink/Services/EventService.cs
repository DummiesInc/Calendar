using BuyMeADrink.Dto.Add;
using BuyMeADrink.Dto.Get;
using BuyMeADrink.Model;
using BuyMeADrink.Utils;
using Supabase;

namespace BuyMeADrink.Services;

public interface IEventService
{
    Task<List<GetEventDto>> GetEvents();
    Task<GetEventDto> GetEvent(int id);
    Task<GetEventDto> AddEvent(AddEventDto addEventDto);
    Task DeleteEvent(int id);
}

public class EventService: IEventService
{
    private readonly Client _supabaseClient;
    
    public EventService(Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<List<GetEventDto>> GetEvents()
    {
        var response = await _supabaseClient.From<Event>().Get();
        var result = response.Models.Select(x => new GetEventDto
        {
            Id = x.Id,
            StartTime = x.StartTime,
            EndTime = x.EndTime,
            Description = x.Description
        }).ToList();
        return result;
    }
    
    public async Task<GetEventDto> GetEvent(int id)
    {
        var response = await _supabaseClient.From<Event>().Where(x => x.Id == id).Get();
        var data = response.Model;

        if (data == null)
        {
            throw new InvalidOperationException("The event you are looking for does not exist");
        }
        return new GetEventDto
        {
            Id = data.Id,
            StartTime = data.StartTime,
            EndTime = data.EndTime,
            Description =  data.Description
        };
    }

    public async Task<GetEventDto> AddEvent(AddEventDto addEventDto)
    {
        // EventType -> event type use to label an event. Example: movie, date, interview, etc...
        var data = new Event
        {
            Description = addEventDto.Description ?? Constant.DefaultString,
            StartTime = addEventDto.StartTime,
            EndTime = addEventDto.EndTime,
            EventType = 1, // Update this later with either enum or a new database table 
        };

        var result = await _supabaseClient.From<Event>().Insert(data);
        
        if (result.Model == null)
        {
            throw new InvalidOperationException("Something bad happened...your event was not added");
        }
        
        return new GetEventDto
        {
            Id = result.Model.Id,
            StartTime = result.Model.StartTime,
            EndTime =  result.Model.EndTime,
            Description = result.Model.Description
        };
    }

    public async Task DeleteEvent(int id)
    {
        var response = await _supabaseClient.From<Event>().Where(x => x.Id == id).Get();
        
        if (response.Model == null)
        {
            throw new InvalidOperationException("The item you are trying to delete does not exist");
        }
        
        await _supabaseClient.From<Event>().Where(x => x.Id == id).Delete();
    }
}