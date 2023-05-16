using AutoMapper;
using QuizApp.Application.Abstractions;
using QuizApp.Application.Users.GetUserById;
using QuizApp.Domain.Repositories;
using QuizApp.Domain.Shared;

namespace QuizApp.Application.Users.GetAllUsers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetAllUsersResponse>
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

        public async Task<Result<GetAllUsersResponse>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            var users = userRepository.SelectAllAsync();
           var allUserResponse = new GetAllUsersResponse(new List<UserResponse>());
            foreach (var item in users)
            {
                allUserResponse.allUsers.Add(
                    mapper.Map<UserResponse>(item));
            }

            return allUserResponse;
        }
    }
}
