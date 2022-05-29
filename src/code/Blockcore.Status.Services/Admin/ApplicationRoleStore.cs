using System.Security.Claims;
using blockcore.status.DataLayer.Context;
using blockcore.status.Entities.Admin;
using blockcore.status.Services.Contracts.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace blockcore.status.Services.Admin;

public class ApplicationRoleStore :
    RoleStore<Role, ApplicationDbContext, int, UserRole, RoleClaim>,
    IApplicationRoleStore
{
    private readonly IdentityErrorDescriber _describer;
    private readonly IUnitOfWork _uow;

    public ApplicationRoleStore(
        IUnitOfWork uow,
        IdentityErrorDescriber describer)
        : base((ApplicationDbContext)uow, describer)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _describer = describer ?? throw new ArgumentNullException(nameof(describer));
    }

    #region BaseClass

    protected override RoleClaim CreateRoleClaim(Role role, Claim claim)
    {
        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        if (claim == null)
        {
            throw new ArgumentNullException(nameof(claim));
        }

        return new RoleClaim
        {
            RoleId = role.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
    }

    #endregion

    #region CustomMethods

    #endregion
}