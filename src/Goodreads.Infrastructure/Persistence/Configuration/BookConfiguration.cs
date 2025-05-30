using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goodreads.Infrastructure.Persistence.Configuration;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
        builder.Property(b => b.ISBN).HasMaxLength(20);
        builder.Property(b => b.Language).HasMaxLength(50);
        builder.Property(b => b.Publisher).HasMaxLength(100);

        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        builder
            .HasMany(b => b.BookGenres)
            .WithOne(bg => bg.Book)
            .HasForeignKey(bg => bg.BookId);
    }
}
