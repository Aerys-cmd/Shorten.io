using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shorten.io.Application.ShortenedUrl.Commands.CreateShortenedUrl;

namespace Shorten.io.Controllers;

[ApiController]
[Route("api/shortened-urls")]
public class ShortenedUrlsController : ControllerBase
{
    private readonly ISender _sender;
    public ShortenedUrlsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> CreateShortenedUrl(CreateShortenedUrlCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Message);
        }

        return Ok($"{HttpContext.Request.Scheme}://{Request.Host}/{result.Value()}");
    }
}
