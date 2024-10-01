using BuyMeADrink.Dto.Add;
using BuyMeADrink.Dto.Get;
using BuyMeADrink.Dto.Request;
using BuyMeADrink.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuyMeADrink.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController: ControllerBase
{
    private readonly ILogger<EventsController> _logger;
    private readonly IEventService _eventService;
    
    public EventsController(ILogger<EventsController> logger, 
        IEventService eventService)
    {
        _logger = logger;
        _eventService = eventService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GetEventDto>>> GetEvents()
    {
        var result = await _eventService.GetEvents();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetEventDto>> GetEvent(int id)
    {
        var result = await _eventService.GetEvent(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<GetEventDto>>> AddEvent(AddEventDto addEventDto)
    {
        var result = await _eventService.AddEvent(addEventDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
        await _eventService.DeleteEvent(id);
        return Ok();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<List<GetEventDto>>> UpdateEvent(int id, UpdateEventDto updateEventDto)
    {
        var res = await _eventService.UpdateEvent(id, updateEventDto);
        return Ok(res);
    }
    // Still need a put and patch endpoint here
}