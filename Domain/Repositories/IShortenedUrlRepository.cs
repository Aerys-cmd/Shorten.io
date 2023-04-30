namespace Shorten.io.Domain.Repositories;

public interface IShortenedUrlRepository
{
    Task<ShortenedUrl?> GetShortenedUrlAsync(string shortenedUrlCode);
    Task<ShortenedUrl> CreateShortenedUrlAsync(ShortenedUrl shortenedUrl);
    Task<ShortenedUrl> UpdateShortenedUrlAsync(ShortenedUrl shortenedUrl);
    Task<bool> CheckIfShortenedUrlCodeExists(string shortenedUrlCode);
    Task<bool> CheckIfOriginalUrlExists(string originalUrl);
    Task<int> SaveChangesAsync();
}
