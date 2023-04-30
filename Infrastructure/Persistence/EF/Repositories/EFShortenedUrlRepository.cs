using Microsoft.EntityFrameworkCore;
using Shorten.io.Domain;
using Shorten.io.Domain.Repositories;

namespace Shorten.io.Infrastructure.Persistence.EF.Repositories;

public class EFShortenedUrlRepository : IShortenedUrlRepository
{
    private readonly ShortenIODbContext _dbContext;
    public EFShortenedUrlRepository(ShortenIODbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CheckIfOriginalUrlExists(string originalUrl)
    {
        return await _dbContext.ShortenedUrls.AnyAsync(x => x.OriginalUrl == originalUrl);
    }

    public async Task<bool> CheckIfShortenedUrlCodeExists(string shortenedUrlCode)
    {
        return await _dbContext.ShortenedUrls.AnyAsync(x => x.ShortenedUrlCode == shortenedUrlCode);
    }

    public async Task<ShortenedUrl> CreateShortenedUrlAsync(ShortenedUrl shortenedUrl)
    {
        await _dbContext.ShortenedUrls.AddAsync(shortenedUrl);

        return shortenedUrl;
    }

    public Task<ShortenedUrl?> GetShortenedUrlAsync(string shortenedUrlCode)
    {
        return _dbContext.ShortenedUrls.FirstOrDefaultAsync(x => x.ShortenedUrlCode == shortenedUrlCode);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public Task<ShortenedUrl> UpdateShortenedUrlAsync(ShortenedUrl shortenedUrl)
    {
        _dbContext.Update(shortenedUrl);
        return Task.FromResult(shortenedUrl);
    }

}
