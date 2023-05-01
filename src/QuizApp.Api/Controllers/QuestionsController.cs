using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Questions.CreateQuestion;
using QuizApp.Application.Questions.GetAllQuestions;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Application.Questions.UpdateQuestion;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;
[Route("api/[controller]")]
public class QuestionsController : ApiController
{
    public QuestionsController(ISender sender,IServiceProvider provider)
        : base(sender, provider)
    { }

    [HttpPost]
    public async ValueTask<IActionResult> CreateQuestion(
        CreateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        Result<CreateQuestionResponse> response =
            await HandleAsync<CreateQuestionResponse,
            CreateQuestionCommand>(request, cancellationToken);

        if(response.IsFailure)
            return HandleFailure(response);

        return Ok(response.Value);
    }

    [HttpPut]
    public async ValueTask<IActionResult> PutQuestion(
        UpdateQuestionCommand request,
        CancellationToken cancellationToken)
    {
        var response = await HandleAsync<UpdateQuestionResponse,
            UpdateQuestionCommand>(request, cancellationToken);

        if (response.IsFailure)
            return HandleFailure(response);

        return Ok(response.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetQuestionById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetQuestionByIdQuery(id);

        var response = await HandleAsync<GetQuestionByIdResponse,
            GetQuestionByIdQuery>(query, cancellationToken);

        if (response.IsFailure)
            return HandleFailure(response);

        return Ok(response.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQuestions(CancellationToken cancellationToken)
    {
        var query = new GetAllQuestionsQuery();

        var response = await HandleAsync<GetAllQuestionsResponse, GetAllQuestionsQuery>
            (query, cancellationToken);

        if(response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response.Value);
    }
}
