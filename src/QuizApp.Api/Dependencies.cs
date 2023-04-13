namespace QuizApp.Api;

public static class Dependencies
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