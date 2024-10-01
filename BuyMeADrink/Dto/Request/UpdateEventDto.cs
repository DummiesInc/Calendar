namespace BuyMeADrink.Dto.Request;

public class UpdateEventDto
{
    public int? EventTypeId { get; set; }

    public string? Description { get; set; }
    
    public DateTime? StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
}