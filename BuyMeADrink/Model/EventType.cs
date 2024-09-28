using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BuyMeADrink.Model;

[Table("EventType")]
public class EventType: BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("is_movie")]
    public bool? IsMovie { get; set; }
    
    [Column("is_date")]
    public bool? IsDate { get; set; }
    
    [Column("is_tv_show")]
    public bool? IsTvShow { get; set; }
    
    [Column("is_random")]
    public bool? IsRandom { get; set; }
    
}