using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goodreads.Infrastructure.Persistence.Configuration;
public class BookShelfConfiguration : IEntityTypeConfiguration<BookShelf>
{
    public void Configure(EntityTypeBuilder<BookShelf> builder)
    {
        builder.HasKey(bs => new { bs.BookId, bs.ShelfId });

        builder.HasOne(bs => bs.Book)
            .WithMany(b => b.BookShelves)
            .HasForeignKey(bs => bs.BookId);

        builder.HasOne(bs => bs.Shelf)
            .WithMany(s => s.BookShelves)
            .HasForeignKey(bs => bs.ShelfId);
    }
}
