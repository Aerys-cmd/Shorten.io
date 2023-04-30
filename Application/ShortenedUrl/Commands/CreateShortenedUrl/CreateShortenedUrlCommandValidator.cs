using FluentValidation;
using Shorten.io.Domain.Helpers;

namespace Shorten.io.Application.ShortenedUrl.Commands.CreateShortenedUrl;

public class CreateShortenedUrlCommandValidator : AbstractValidator<CreateShortenedUrlCommand>
{
    public CreateShortenedUrlCommandValidator()
    {
        RuleFor(x => x.Url)
            .MaximumLength(2048)
            .Must(UrlHelper.IsValidUrl)
            .WithMessage("Invalid url");

        RuleFor(x => x.CustomUrlCode)
            .MaximumLength(6)
            .Must(x => x is null ? true : UrlHelper.IsValidUrl($"https://www.shorten.io/{x}"))
            .WithMessage("Shortened code should be valid.");

    }
}
