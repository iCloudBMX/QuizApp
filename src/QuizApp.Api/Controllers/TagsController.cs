using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Questions.GetQuestionById;
using QuizApp.Application.Tags.CreateTag;
using QuizApp.Application.Tags.GetQuestionsByTag;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Application.Tags.GetTagsByTesterId;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/tags")]
public class TagsController : ApiController
{
    public TagsController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag(
        CreateTagCommand tag,
        CancellationToken cancellationToken)
    {
        var response = await Sender.Send(tag, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }
    /*
    [HttpGet("id:guid")]
    public async Task<IActionResult> GetTagById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetTagByIdQuery(id);

        Result<TagResponse> response = await Sender.Send(query, cancellationToken);

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }

        return Ok(response);
    }
    */
    [HttpGet("TesterId:guid")]
    public async Task<IActionResult> GetTagsByTesterId(
        Guid testerId,
        CancellationToken cancellation)
    {
        var query = new GetTagsByTesterIdQuery(testerId);

        Result<IList<TagResponse>> result = await Sender.Send(query, cancellation);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }

    [HttpGet("/questions/TagId:guid")]
    public async Task<IActionResult> GetQuestionsByTagId(
        Guid tagId,
        CancellationToken cancellation)
    {
        var query = new GetQuestionsByTagQuery(tagId);

        Result<IList<GetQuestionByIdResponse>> result = await Sender.Send(query, cancellation);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
