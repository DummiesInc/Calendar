using BuyMeADrink.Dto.Add;
using BuyMeADrink.Dto.Get;
using BuyMeADrink.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuyMeADrink.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController: ControllerBase
{
    private readonly ILogger<EventController> _logger;
    private readonly IEventService _eventService;
    
    public EventController(ILogger<EventController> logger, 
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

    [HttpGet("id")]
    public async Task<ActionResult<GetEventDto>> GetEvent([FromQuery] int id)
    {
        var result = await _eventService.GetEvent(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GetEventDto>> AddEvent(AddEventDto addEventDto)
    {
        var result = await _eventService.AddEvent(addEventDto);
        return Ok(result);
    }

    [HttpDelete("id")]
    public async Task<ActionResult> DeleteEvent([FromQuery] int id)
    {
        await _eventService.DeleteEvent(id);
        return Ok();
    }
    
    // Still need a put and patch endpoint here
}