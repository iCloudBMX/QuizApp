using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Application.AutoMapper;
using QuizApp.Application.ExamAttendants;
using QuizApp.Application.ExamAttendants.Validation;
using QuizApp.Application.Exams.CreateExam;
using QuizApp.Application.Exams.Validation;
using QuizApp.Application.OtpCodes.Validation;
using QuizApp.Application.Questions.CreateQuestion;
using QuizApp.Application.Questions.Validation;
using QuizApp.Application.Users.Register;
using QuizApp.Application.Users.Validation;

namespace QuizApp.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(Dependencies).Assembly);

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IValidator<CreateExamAttendantCommand>, ExamAttendantValidator>();

        services.AddScoped<IValidator<CreateExamCommand>, CreateExamValidator>();

        services.AddScoped<IValidator<CreateQuestionCommand>, CreateQuestionValidator>();

        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserValidator>();

        services.AddScoped<IValidator<VerifyOtpCodeCommand>, VerifyOtpCodeValidator>();

        return services;
    }
}
