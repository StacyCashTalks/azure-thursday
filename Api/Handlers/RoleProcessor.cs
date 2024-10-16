using StacyClouds.SwaAuth.Models;

namespace Api.Handlers;

internal class RoleProcessor : IRoleProcessor
{
    public List<string> ProcessRoles(ClientPrincipal clientPrincipal)
    {
        var roles = new List<string>
            {
                clientPrincipal.UserDetails?.Replace(" ", "_") ?? "No_User_Details",
                clientPrincipal.IdentityProvider ?? "No_Identity",
                "authorised"
            };

        return roles;
    }
}
