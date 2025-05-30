using Goodreads.Domain.Constants;
using Goodreads.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Goodreads.Infrastructure.Persistence.Seeders;
internal class RolesSeeder(ApplicationDbContext dbContext, UserManager<User> userManager) : ISeeder
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync()
            && !await dbContext.Roles.AnyAsync())
        {
            var roles = GetRoles();
            await dbContext.Roles.AddRangeAsync(roles);
            await dbContext.SaveChangesAsync();

            // Seed an admin user (testing)
            var adminUser = new User
            {
                UserName = "admin",
                Email = "admin@goodreads.com",
            };
            var createResult = await userManager.CreateAsync(adminUser, "admin123");
            if (createResult.Succeeded)
                await userManager.AddToRoleAsync(adminUser, Roles.Admin);
        }
    }

    private List<IdentityRole> GetRoles()
    {
        return new List<IdentityRole>
        {
            new IdentityRole { Name = Roles.User , NormalizedName = Roles.User },
            new IdentityRole { Name = Roles.Admin , NormalizedName = Roles.Admin }
        };
    }
}
