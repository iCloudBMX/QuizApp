using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Domain.Shared;
using System.Text;

namespace QuizApp.Api.Controllers;

//[ApiController]
public abstract class ApiController : ControllerBase
{
    private readonly ISender sender;
    private readonly IServiceProvider serviceProvider;

    protected ApiController(
        ISender sender,
        IServiceProvider serviceProvider)
    {
        this.sender = sender;
        this.serviceProvider = serviceProvider;
    }

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest,
                        result.Error,
                        validationResult.Errors)),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        StatusCodes.Status400BadRequest,
                        result.Error))
        };

    protected async Task<Result<TResponse>> HandleAsync<TResponse, TRequest>(
        TRequest request,
        CancellationToken cancellationToken)
            where TRequest : IRequest<Result<TResponse>>
    {
        var validator = this.serviceProvider
            .GetService<IValidator<TRequest>>();

        if (validator is not null)
        {
            var validationResultResult = await validator
                .ValidateAsync(request, cancellationToken);

            if (validationResultResult.IsValid is false)
            {
                StringBuilder validateErrors = new StringBuilder();
                foreach (var item in validationResultResult.Errors)
                {
                    validateErrors.AppendLine(item.ErrorMessage);
                }

                var errors = new Error("Validation error", validateErrors.ToString());

                return Result.Failure<TResponse>(errors);
            }
        }

        return await this.sender
            .Send(request, cancellationToken);
    }

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
