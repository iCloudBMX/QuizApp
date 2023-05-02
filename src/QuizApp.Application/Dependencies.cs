using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Application.AutoMapper;
using QuizApp.Application.Helpers.Generatewt;
using QuizApp.Application.Helpers.PasswordHashers;
using System.Reflection;

namespace QuizApp.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(typeof(Dependencies).Assembly);

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IPasswordHasher,PasswordHasher>();


        services.Configure<JwtOption>(configuration.GetSection("JwtSettings"));
        services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();

        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(item => item.GetInterfaces()
            .Where(i => i.IsGenericType).
            Any(i => i.GetGenericTypeDefinition() == typeof(IValidator<>)) && !item.IsAbstract && !item.IsInterface)
            .ToList()
            .ForEach(assignedTypes =>
            {
                var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IValidator<>));
                services.AddScoped(serviceType, assignedTypes);
            });

        return services;
    }
}
