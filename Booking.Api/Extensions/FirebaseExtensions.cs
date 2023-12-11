using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FirebaseAdminAuthentication.DependencyInjection.Services;

namespace Booking.Api.Extensions;

public static class FirebaseExtensions
{
    public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });

        services.AddScoped<FirebaseAuthenticationFunctionHandler>();

        return services;
    }
}
