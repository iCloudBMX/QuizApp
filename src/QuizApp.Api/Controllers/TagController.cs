using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ApiController
    {
        public TagController(ISender sender) : base(sender)
        {
        }
    }
}
