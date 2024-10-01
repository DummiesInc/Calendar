using AutoMapper;
using BuyMeADrink.Dto.Add;
using BuyMeADrink.Dto.Get;
using BuyMeADrink.Dto.Request;
using BuyMeADrink.Model;
using BuyMeADrink.Utils;
using Supabase;

namespace BuyMeADrink.Services;

public interface IEventService
{
    Task<List<GetEventDto>> GetEvents();
    Task<GetEventDto> GetEvent(int id);
    Task<List<GetEventDto>> AddEvent(AddEventDto addEventDto);
    Task<List<GetEventDto>> UpdateEvent(int id, UpdateEventDto updateEventDto);
    Task DeleteEvent(int id);
}

public class EventsService: IEventService
{
    private readonly Client _supabaseClient;
    private readonly IMapper _mapper;
    public EventsService(
        IMapper mapper,
        Client supabaseClient)
    {
        _mapper = mapper;
        _supabaseClient = supabaseClient;
    }

    public async Task<List<GetEventDto>> GetEvents()
    {
        var response = await _supabaseClient.From<Event>().Get();
        var mappedEvents = _mapper.Map<List<GetEventDto>>(response.Models);
        return mappedEvents;
    }
    
    public async Task<GetEventDto> GetEvent(int id)
    {
        var response = await _supabaseClient.From<Event>().Where(x => x.Id == id).Get();
        var data = response.Model;

        if (data == null)
        {
            throw new InvalidOperationException("The event you are looking for does not exist");
        }
        return _mapper.Map<GetEventDto>(data);
    }

    public async Task<List<GetEventDto>> AddEvent(AddEventDto addEventDto)
    {
        
        var eventType = await _supabaseClient.From<EventType>().Where(x => x.Id == addEventDto.EventTypeId).Get();
        // EventType -> event type use to label an event. Example: movie, date, interview, etc...

        if (eventType.Model == null)
        {
            throw new InvalidOperationException("Invalid event type");
        }
        
        var data = new Event
        {
            Description = addEventDto.Description ?? Constant.DefaultString,
            StartTime = addEventDto.StartTime,
            EndTime = addEventDto.EndTime,
            EventTypeId = eventType.Model.Id, 
        };

        var result = await _supabaseClient.From<Event>().Insert(data);
        
        if (result.Model == null)
        {
            throw new InvalidOperationException("Something bad happened...your event was not added");
        }

        var response = await _supabaseClient.From<Event>().Get();
        return _mapper.Map<List<GetEventDto>>(response.Models);
        
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
    
    public async Task<List<GetEventDto>> UpdateEvent(int id, UpdateEventDto updateEventDto)
    {
        var response = await _supabaseClient.From<Event>().Where(x => x.Id == id).Get();
        
        if (response.Model == null)
        {
            throw new InvalidOperationException("The item you are trying to update does not exist");
        }

        await _supabaseClient.From<Event>().Where(x => x.Id == id)
            .Set(x => x.StartTime, updateEventDto.StartTime ?? response.Model.StartTime)
            .Set(x => x.EndTime, updateEventDto.EndTime ?? response.Model.EndTime)
            .Set(x => x.Description, updateEventDto.Description ?? response.Model.Description)
            .Set(x => x.EventTypeId, updateEventDto.EventTypeId ?? response.Model.EventTypeId)
            .Update();
        
        var eventList = await _supabaseClient.From<Event>().Get();
        return _mapper.Map<List<GetEventDto>>(eventList.Models);
        
    }
}