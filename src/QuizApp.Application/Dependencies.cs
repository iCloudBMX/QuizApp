using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Application.AutoMapper;
using QuizApp.Application.ExamAttendants;
using QuizApp.Application.ExamAttendants.Validation;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Exams.Validation;

namespace QuizApp.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(Dependencies).Assembly);

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IValidator<CreateExamCommand>, ExamValidator>();
        services.AddScoped<IValidator<CreateExamAttendantCommand>, ExamAttendantValidator>();
        return services;
    }
}
