using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using kojira.WebApi.Identity.Models;
using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Identity;

namespace kojira.WebApi.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e => e.ToTable("Users"));
        builder.Entity<IdentityRole>(e => e.ToTable("Roles"));
        builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
        builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
        builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
        builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));
    }
}

