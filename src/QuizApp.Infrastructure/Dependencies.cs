using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Domain.Interfaces;
using QuizApp.Domain.Repositories;
using QuizApp.Infrastructure.Options;
using QuizApp.Infrastructure.Persistence;
using QuizApp.Infrastructure.Persistence.Repositories;
using QuizApp.Infrastructure.Services;

namespace QuizApp.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistence(services, configuration);

        return services;
    }

    private static void AddPersistence(
        IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>(options =>
        {
            string connectionString = configuration
                .GetConnectionString("DefaultConnectionString");

            options.UseSqlServer(
                connectionString: connectionString,
                sqlServerOptionsAction: options => options.EnableRetryOnFailure());
        });

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        services.AddTransient<IEmailService, EmailService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITagRepository, TagRepository>();
        services.AddTransient<IQuestionOptionRepository,QuestionOptionRepository>();
        services.AddTransient<IExamRepository,ExamRepository>();
        services.AddTransient<IQuestionRepository,QuestionRepository>();
        services.AddTransient<IExamAttendantRepository,ExamAttendantRepository>();
        services.AddTransient<IOtpCodeRepository, OtpCodeRepository>();
    }
}
