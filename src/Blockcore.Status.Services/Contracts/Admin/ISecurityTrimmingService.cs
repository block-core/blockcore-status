using System.Security.Claims;

namespace blockcore.status.Services.Contracts.Admin;

public interface ISecurityTrimmingService
{
    bool CanCurrentUserAccess(string area, string controller, string action);
    bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action);
}