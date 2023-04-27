using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Tags.GetTagById;
using QuizApp.Domain.Shared;

namespace QuizApp.Api.Controllers;

[Route("api/tags")]
public class TagsController : ApiController
{
    public TagsController(ISender sender,IServiceProvider provider)
        : base(sender, provider)
    {
    }
    [HttpGet("id:guid")]
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
}
