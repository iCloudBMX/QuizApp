using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Questions.CreateQuestion;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;
[Route("api/[controller]")]
public class QuestionController : ApiController
{
    public QuestionController(ISender sender)
        : base(sender)
    { }

    [HttpPost]
    public async ValueTask<IActionResult> CreateQuestion(
        CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        Result<CreateQuestionResponse> response =
            await Sender.Send(request, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetQuestionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetQuestionByIdQuery(id);

        Result<GetQuestionByIdResponse> response = await Sender.Send(query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }
}
