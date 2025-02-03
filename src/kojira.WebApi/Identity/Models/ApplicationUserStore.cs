using kojira.WebApi.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace kojira.WebApi.Identity.Models;

public class ApplicationUserStore : UserStore<ApplicationUser, IdentityRole, ApplicationDbContext, string>
{
    public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
        : base(context, describer)
    {
    }

    public override Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public override Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        user.UserName = string.Empty;
        return base.CreateAsync(user, cancellationToken);
    }
}
