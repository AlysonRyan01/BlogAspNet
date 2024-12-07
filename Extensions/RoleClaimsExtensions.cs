using System.Security.Claims;
using BlogAspNet.Models;

namespace BlogAspNet.Extensions;

public static class RoleClaimsExtensions
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
        };
        foreach (var role in user.Roles)
        {
           result.Add(new Claim(ClaimTypes.Role, role.Slug));
        }
        return result;
    }
}