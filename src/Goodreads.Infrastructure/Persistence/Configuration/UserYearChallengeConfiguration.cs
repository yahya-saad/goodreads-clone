using Goodreads.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Goodreads.Infrastructure.Persistence.Configuration;
public class UserYearChallengeConfiguration : IEntityTypeConfiguration<UserYearChallenge>
{
    public void Configure(EntityTypeBuilder<UserYearChallenge> builder)
    {
        builder.HasKey(uc => new { uc.UserId, uc.Year });
    }
}