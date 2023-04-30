using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shorten.io.Domain;
using Shorten.io.Infrastructure.Persistence.EF.Configurations;

namespace Shorten.io.Infrastructure.Persistence.EF;

public class ShortenIODbContext : DbContext
{

    public ShortenIODbContext(DbContextOptions<ShortenIODbContext> options) : base(options)
    {

    }

    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Since we're using InMemoryDb this configuration does not work.
        modelBuilder
            .ApplyConfiguration(
                new ShortenedUrlConfiguration()
            );

        base.OnModelCreating(modelBuilder);
    }

}
