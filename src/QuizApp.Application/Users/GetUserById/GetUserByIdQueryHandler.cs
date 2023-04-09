using QuizApp.Application.Abstractions;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Errors;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.GetUserById;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IUserRepository userRepository;

    public GetUserByIdQueryHandler(
        IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Result<UserResponse>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        User? maybeUser = await this.userRepository.SelectAsync(
            entityIds: request.UserId,
            cancellationToken: cancellationToken);

        if(maybeUser is null)
        {
            Result.Failure<UserResponse>(
                DomainErrors.User.NotFound(request.UserId));
        }

        var response = new UserResponse(maybeUser.Id, maybeUser.Name);

        return response;
    }
}
