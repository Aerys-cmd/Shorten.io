using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shorten.io.Domain;

namespace Shorten.io.Infrastructure.Persistence.EF.Configurations;

public class ShortenedUrlConfiguration : IEntityTypeConfiguration<ShortenedUrl>
{
    public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
    {

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalUrl)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(x => x.ShortenedUrlCode)
            .HasMaxLength(6);

        builder.Property(x => x.RequestCount);

        builder.HasIndex(x => x.ShortenedUrlCode)
            .IsUnique();

    }
}
