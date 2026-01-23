using TravelWebApp.Services;
using TravelWebApp.Services.Interfaces;
using TravelWebApp.Services.Implementations;

namespace TravelWebApp.Extensions;

public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITourService, TourService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IBookingService,BookingService>();
            services.AddScoped<IHotelService,HotelService>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IUserServiceCrud, UserServiceCrud>();
            services.AddScoped<IReviewService,ReviewService>();
            return services;
        }
    }
