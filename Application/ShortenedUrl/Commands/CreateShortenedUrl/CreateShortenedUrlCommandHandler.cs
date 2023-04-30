using MediatR;
using Shorten.io.Application.Common;
using Shorten.io.Domain;
using Shorten.io.Domain.Repositories;

namespace Shorten.io.Application.ShortenedUrl.Commands.CreateShortenedUrl;

public class CreateShortenedUrlCommandHandler : IRequestHandler<CreateShortenedUrlCommand, Result<string>>
{
    private readonly IShortenedUrlRepository _shortenedUrlRepository;

    public CreateShortenedUrlCommandHandler(IShortenedUrlRepository shortenedUrlRepository)
    {
        _shortenedUrlRepository = shortenedUrlRepository;
    }

    public async Task<Result<string>> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
    {
        var isOriginalUrlExists = await _shortenedUrlRepository.CheckIfOriginalUrlExists(request.Url);

        if (isOriginalUrlExists)
        {
            return Result.Failure<string>("The url is already shortened.");
        }

        var customUrlCode = request.CustomUrlCode;

        if (!string.IsNullOrWhiteSpace(customUrlCode))
        {
            var isCustomUrlCodeExists = await _shortenedUrlRepository.CheckIfShortenedUrlCodeExists(customUrlCode);

            if (isCustomUrlCodeExists)
            {
                return Result.Failure<string>("Requested custom url code already exists.");
            }

            var customShortenedUrl = Domain.ShortenedUrl.Create(request.Url, customUrlCode);

            await _shortenedUrlRepository.CreateShortenedUrlAsync(customShortenedUrl);

            await _shortenedUrlRepository.SaveChangesAsync();

            return Result.Success(customUrlCode);

        }

        var shortenedUrl = Domain.ShortenedUrl.Create(request.Url);

        await _shortenedUrlRepository.CreateShortenedUrlAsync(shortenedUrl);

        await _shortenedUrlRepository.SaveChangesAsync();

        return Result.Success(shortenedUrl.ShortenedUrlCode);

    }
}
