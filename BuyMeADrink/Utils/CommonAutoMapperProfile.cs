using AutoMapper;
using BuyMeADrink.Dto.Add;
using BuyMeADrink.Dto.Get;
using BuyMeADrink.Dto.Request;
using BuyMeADrink.Model;

namespace BuyMeADrink.Utils;

public class CommonAutoMapperProfile: Profile
{
 public CommonAutoMapperProfile()
 {
   //Gets
   CreateMap<Event, GetEventDto>().ReverseMap();
   
   //Adds
   CreateMap<Event, AddEventDto>().ReverseMap();
   
   //Update
   CreateMap<Event, UpdateEventDto>().ReverseMap();
   
   
 }   
}