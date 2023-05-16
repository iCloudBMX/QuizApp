
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application;
using QuizApp.Application.Users.Login;
using QuizApp.Application.Users.Register;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/auth")]
public class AuthController : ApiController
{
    public AuthController(
        ISender sender,
        IServiceProvider serviceProvider) : base(sender, serviceProvider)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserCommand registerUserCommand,
        CancellationToken cancellationToken)
    {
        var result = await HandleAsync<Guid, RegisterUserCommand>(
            registerUserCommand, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] VerifyOtpCodeCommand verifyOtpCodeCommand,
        CancellationToken cancellationToken)
    {
        Result<Guid> result = await HandleAsync<Guid, VerifyOtpCodeCommand>(
            verifyOtpCodeCommand, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result.Value);
    }

    [HttpPost("resend-email")]
    public async Task<IActionResult> ResendEmailByUserId(
       [FromBody] ResendEmailCommand userId,
        CancellationToken cancellationToken)
    {
        var query = new ResendEmailCommand(userId.userId);
        var response = await HandleAsync<Guid, ResendEmailCommand>(
            query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult>Login(
       [FromBody] LoginQuery loginQuery,
       CancellationToken cancellationToken)
    {
        var response =await HandleAsync<LoginQueryResponse, LoginQuery>(
            loginQuery,cancellationToken);

        if(response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }
}