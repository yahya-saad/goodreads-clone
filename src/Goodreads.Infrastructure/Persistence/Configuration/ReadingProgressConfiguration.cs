using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goodreads.Infrastructure.Persistence.Configuration;
public class ReadingProgressConfiguration : IEntityTypeConfiguration<ReadingProgress>
{
    public void Configure(EntityTypeBuilder<ReadingProgress> builder)
    {
        builder.HasKey(rp => new { rp.BookId, rp.UserId });

        builder.HasOne(rp => rp.Book)
            .WithMany()
            .HasForeignKey(rp => rp.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}