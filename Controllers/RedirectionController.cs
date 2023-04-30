using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Shorten.io.Application.Common;
using Shorten.io.Application.ShortenedUrl.Queries.GetOriginalUrlByCode;

namespace Shorten.io.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectionController : ControllerBase
    {
        private readonly ISender _sender;
        public RedirectionController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{shortenedUrlCode}")]
        public async Task<IActionResult> Get(string shortenedUrlCode)
        {
            var query = new GetOriginalUrlByCodeQuery(shortenedUrlCode);

            var result = await _sender.Send(query);

            if (result.IsFailure)
            {
                return result.Message == Messages.NotFound ?
                    NotFound() : BadRequest(result.Message);
            }

            var uri = new Uri(result.Value());

            var absoluteUrl = new UriBuilder(uri)
            {
                Scheme = uri.Scheme,
                // Requested host may contain special characters, so we need to escape it.
                // Ex :/ Chinese or Indian characters
                Host = Uri.EscapeDataString(uri.Host),
                Path = (uri.AbsolutePath),
                Query = (uri.Query)
            };

            return Redirect(absoluteUrl.ToString());
        }
    }
}