using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using QuizApp.Application.Helpers.Generatewt;
using QuizApp.Application.OtpCodes;

namespace QuizApp.Api.Controllers
{
    [Route("api/otps")]
   
    public class OtpController : ApiController
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public OtpController(
            ISender sender,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor) :
            base(sender, serviceProvider)
        {
            this.httpContextAccessor=httpContextAccessor;
        }
        [Authorize]
        [HttpPost("send")]
        public async ValueTask<IActionResult> SendOtpCode(
            CancellationToken cancellationToken)
        {
            var id = HttpContext.User.Claims.FirstOrDefault(c => c.Type=="id")?.Value;

            var email = httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(p=>p.Type==CustomClaimNames.Email);

            if ( id is null || email is null )
                return BadRequest();

            var command = new SendOtpCommand(
                Guid.Parse(id),
                email.Value);

            var result = await HandleAsync<bool, SendOtpCommand>(
                command, cancellationToken);

            if ( result.IsFailure )
                return HandleFailure(result);

            return Ok(result.Value);
        }
    }
}
