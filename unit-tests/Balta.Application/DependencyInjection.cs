using Balta.Domain.AccountContext.Services;
using Balta.Domain.AccountContext.Services.Abstractions;
using FluentValidation;

namespace Balta.Application;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            // x.AddOpenBehavior(typeof(LoggingBehavior<,>));
            // x.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<IAccountDeniedService, AccountDeniedService>();
        
        return services;
    }
}