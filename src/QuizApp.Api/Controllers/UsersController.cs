using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Users.GetUserById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/users")]
public class UsersController : ApiController
{
    public UsersController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        Result<UserResponse> response = await Sender.Send(query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }
}
