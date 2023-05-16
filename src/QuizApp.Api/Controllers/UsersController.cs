using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Application.Tags.GetTagsByTesterId;
using QuizApp.Application.Users.GetAllUsers;
using QuizApp.Application.Users.GetUserById;

namespace QuizApp.Api.Controllers;

[Route("api/users")]
public class UsersController : ApiController
{
    public UsersController(
        ISender sender,
        IServiceProvider serviceProvider) : base(sender, serviceProvider)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(
        CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();

        var response = await HandleAsync
            <GetAllUsersResponse, GetAllUsersQuery>(
            query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
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

        return Ok(response.Value);
    }

    [HttpGet("{id:guid}/tags")]
    public async Task<IActionResult> GetTagsByTesterId(
        Guid testerId,
        CancellationToken cancellation)
    {
        var query = new GetTagsByTesterIdQuery(testerId);

        var result = await HandleAsync<IList<TagResponse>,
            GetTagsByTesterIdQuery>(query, cancellation);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
