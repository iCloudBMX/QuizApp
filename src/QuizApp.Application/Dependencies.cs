using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Application.AutoMapper;
using System.Reflection;

namespace QuizApp.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(Dependencies).Assembly);

        services.AddAutoMapper(typeof(MappingProfile));

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
