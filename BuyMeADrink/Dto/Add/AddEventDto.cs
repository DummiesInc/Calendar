using BuyMeADrink.Utils;

namespace BuyMeADrink.Dto.Add;

public class AddEventDto
{
    public int EventTypeId { get; set; }

    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
}