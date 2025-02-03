using Microsoft.AspNetCore.Identity;

namespace kojira.WebApi.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Id = Guid.NewGuid().ToString("N");
    }

    [ProtectedPersonalData]
    public override string? UserName { get => base.UserName; set => base.UserName = value; }
}

