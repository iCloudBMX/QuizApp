using AutoMapper;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Users.GetUserById;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IList<UserResponse>>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(
            IUserRepository userRepository, 
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public Task<Result<IList<UserResponse>>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = userRepository.SelectAllAsync(cancellationToken);
            IList<UserResponse> allUserResponse = new List<UserResponse>();
            foreach (var item in users.Result)
            {
                allUserResponse.Add(
                    mapper.Map<UserResponse>(item));
            }

            return (Task<Result<IList<UserResponse>>>)allUserResponse;
        }
    }
}
