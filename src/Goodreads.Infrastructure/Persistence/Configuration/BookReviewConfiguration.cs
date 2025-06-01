using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goodreads.Infrastructure.Persistence.Configuration;
public class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
{
    public void Configure(EntityTypeBuilder<BookReview> builder)
    {
        builder.HasKey(br => new { br.UserId, br.BookId });

        builder.Property(br => br.ReviewText)
               .HasMaxLength(2500);

        builder.HasOne(br => br.User)
               .WithMany(u => u.BookReviews)
               .HasForeignKey(br => br.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(br => br.Book)
               .WithMany(b => b.BookReviews)
               .HasForeignKey(br => br.BookId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}