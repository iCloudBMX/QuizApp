using MediatR;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Abstractions;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
