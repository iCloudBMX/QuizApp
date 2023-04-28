using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application;
using QuizApp.Application.Abstractions;
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
        Result<Guid> result = await HandleAsync<Guid, VerifyOtpCodeCommand>(
            verifyOtpCodeCommand, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
