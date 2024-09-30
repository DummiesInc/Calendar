using BuyMeADrink.Services;

namespace BuyMeADrink.Utils;

public static class CommonConfiguration
{
    public static IServiceCollection ConfigureCommonServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CommonConfiguration));
        services.AddScoped<IEventService, EventsService>();
        return services;
    }
}