namespace QuizApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApis(
        this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}