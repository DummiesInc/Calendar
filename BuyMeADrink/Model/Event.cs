using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BuyMeADrink.Model;

[Table("Event")]
public class Event: BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    
    [Column("event_type_id")]
    public int EventTypeId { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("start_time")]
    public DateTime StartTime { get; set; }
    
    [Column("end_time")]
    public DateTime EndTime { get; set; }
}