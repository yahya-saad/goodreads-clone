using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goodreads.Infrastructure.Persistence.Configuration;
internal class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired().
            HasMaxLength(100);

        builder.Property(a => a.Bio)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(a => a.ProfilePictureUrl).HasMaxLength(500);

        builder.Property(a => a.ProfilePictureBlobName).HasMaxLength(200);

        builder.HasIndex(a => a.UserId).IsUnique();

        builder.HasOne(a => a.User)
            .WithOne(u => u.ClaimedAuthorProfile)
            .HasForeignKey<Author>(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(a => a.Books)
       .WithOne(b => b.Author)
       .HasForeignKey(b => b.AuthorId)
       .OnDelete(DeleteBehavior.NoAction);
    }
}
