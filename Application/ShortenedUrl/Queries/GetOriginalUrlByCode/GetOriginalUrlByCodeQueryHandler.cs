using MediatR;
using Shorten.io.Application.Common;
using Shorten.io.Application.ShortenedUrl.Queries.GetOriginalUrlByCode;
using Shorten.io.Domain.Repositories;

namespace Shorten.io.Application.ShortenedUrl.Queries.GetOriginalUrlByShortenedCode;

public class GetOriginalUrlByCodeQueryHandler : IRequestHandler<GetOriginalUrlByCodeQuery, Result<string>>
{
    private readonly IShortenedUrlRepository _shortenedUrlRepository;

    public GetOriginalUrlByCodeQueryHandler(IShortenedUrlRepository shortenedUrlRepository)
    {
        _shortenedUrlRepository = shortenedUrlRepository;
    }
    public async Task<Result<string>> Handle(GetOriginalUrlByCodeQuery request, CancellationToken cancellationToken)
    {
        var shortenedUrl = await _shortenedUrlRepository.GetShortenedUrlAsync(request.ShortenedCode);

        if (shortenedUrl is null)
        {
            return Result.Failure<string>(Messages.NotFound);
        }

        shortenedUrl.ShortenedUrlVisited();

        await _shortenedUrlRepository.UpdateShortenedUrlAsync(shortenedUrl);

        await _shortenedUrlRepository.SaveChangesAsync();


        return Result.Success(shortenedUrl.OriginalUrl);
    }
}
