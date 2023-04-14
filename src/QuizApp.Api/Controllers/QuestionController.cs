using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Application.Users.GetUserById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;
[Route("api/[controller]")]
public class QuestionController : ApiController
{
    public QuestionController(ISender sender) 
        : base(sender)
    { }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetQuestionById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetQuestionByIdQuery(id);

        Result<QuestionResponse> response = await Sender.Send(query, cancellationToken);

        return 
            response.IsSuccess ? Ok(response) : NotFound(response.Error);
    }
}
