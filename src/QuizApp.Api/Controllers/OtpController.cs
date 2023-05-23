using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.OtpCodes;
using System.Security.Claims;

namespace QuizApp.Api.Controllers
{
    [Route("api/otps")]

    [Authorize]
    public class OtpController : ApiController
    {
        public OtpController(
            ISender sender,
            IServiceProvider serviceProvider) :
            base(sender, serviceProvider)
        { }

        [HttpPost("send")]
        public async ValueTask<IActionResult> SendOtpCode(
            CancellationToken cancellationToken)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var email = HttpContext.User.Claims
                .FirstOrDefault(p => p.Type == ClaimTypes.Email);

            if (id is null || email is null)
                return BadRequest();

            var command = new SendOtpCommand(
                Guid.Parse(id),
                email.Value);

            var result = await HandleAsync<bool, SendOtpCommand>(
                command, cancellationToken);

            if (result.IsFailure)
                return HandleFailure(result);

            return Ok(result.Value);
        }
    }
}
