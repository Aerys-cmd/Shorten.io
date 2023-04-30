using MediatR;
using Shorten.io.Application.Common;

namespace Shorten.io.Application.ShortenedUrl.Commands.CreateShortenedUrl;

public record CreateShortenedUrlCommand(
    string Url,
    string? CustomUrlCode = null
    ) : IRequest<Result<string>>;

