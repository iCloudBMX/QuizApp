using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application;
using QuizApp.Application.Users.Register;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/auth")]
public class AuthController : ApiController
{
    public AuthController(ISender sender) : base(sender)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] RegisterUserCommand registerUserCommand,
        CancellationToken cancellationToken)
    {
        Result<Guid> result = await Sender
            .Send(registerUserCommand, cancellationToken);

        if(result.IsFailure)
        {
            return HandleFailure(result); 
        }

        return Ok(result);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] VerifyOtpCodeCommand verifyOtpCodeCommand,
        CancellationToken cancellationToken)
    {
        Result<Guid> result = await Sender
            .Send(verifyOtpCodeCommand, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
