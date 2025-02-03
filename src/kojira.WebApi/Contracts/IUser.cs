using System.Security.Claims;

namespace kojira.WebApi.Contracts;

public interface IUser
{
    string Name { get; }
    
    bool IsAuthenticated();

    IEnumerable<Claim> GetClaimsIdentity();
}
