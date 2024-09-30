using AutoMapper;
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
        // return new GetEventDto
        // {
        //     Id = data.Id,
        //     StartTime = data.StartTime,
        //     EndTime = data.EndTime,
        //     Description =  data.Description,
        //     EventTypeId = data.EventTypeId,
        // };
        return _mapper.Map<GetEventDto>(data);
    }

    public async Task<GetEventDto> AddEvent(AddEventDto addEventDto)
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

        return _mapper.Map<GetEventDto>(result.Model);
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