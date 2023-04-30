using MediatR;
using Shorten.io.Application.Common;

namespace Shorten.io.Application.ShortenedUrl.Queries.GetOriginalUrlByCode;

public record GetOriginalUrlByCodeQuery(
    string ShortenedCode
    ) : IRequest<Result<string>>;
