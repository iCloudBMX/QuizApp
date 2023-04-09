using MediatR;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}