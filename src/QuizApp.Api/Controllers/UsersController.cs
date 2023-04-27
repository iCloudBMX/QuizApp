using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Users.GetUserById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/users")]
public class UsersController : ApiController
{
    public UsersController(
        ISender sender,
        IServiceProvider serviceProvider) : base(sender, serviceProvider)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        var response = await HandleAsync<UserResponse, GetUserByIdQuery>(
            query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }
}
