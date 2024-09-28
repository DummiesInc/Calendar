namespace BuyMeADrink.Dto.Get;

public class GetEventDto
{
    public int Id { get; set; }
    
    public int EventType { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
}