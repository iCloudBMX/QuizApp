using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Application.Tags.CreateTag;
using QuizApp.Application.Tags.DeleteTag;
using QuizApp.Application.Tags.GetQuestionsByTag;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/tags")]
public class TagsController : ApiController
{
    public TagsController(ISender sender, IServiceProvider provider)
        : base(sender, provider)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(
        CreateTagCommand tag,
        CancellationToken cancellationToken)
    {
        var response = await HandleAsync<TagResponse,
            CreateTagCommand>(tag, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTagById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetTagByIdQuery(id);

        Result<TagResponse> response = await HandleAsync<TagResponse,
            GetTagByIdQuery>(query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }

    [HttpGet("{id:guid}/questions")]
    public async Task<IActionResult> GetQuestionsByTagId(
        Guid id,
        CancellationToken cancellation)
    {
        var query = new GetQuestionsByTagQuery(id);

        var result = await HandleAsync<IList<GetQuestionByIdResponse>,
            GetQuestionsByTagQuery>(query, cancellation);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTag(
        Guid id,
        CancellationToken cancellation)
    {
        var query = new DeleteTagCommand(id);
        var result = await HandleAsync<Guid, DeleteTagCommand>(query, cancellation);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return Ok(result.Value);
    }
}
