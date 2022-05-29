using System.Security.Claims;
using System.Security.Principal;
using BlockcoreStatus.Entities.Admin;
using BlockcoreStatus.Services.Contracts.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlockcoreStatus.Services.Admin;


public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    public static readonly string PhotoFileName = nameof(PhotoFileName);

    private readonly IOptions<IdentityOptions> _optionsAccessor;
    private readonly IApplicationRoleManager _roleManager;
    private readonly IApplicationUserManager _userManager;

    public ApplicationClaimsPrincipalFactory(
        IApplicationUserManager userManager,
        IApplicationRoleManager roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base((UserManager<User>)userManager, (RoleManager<Role>)roleManager, optionsAccessor)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _optionsAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(optionsAccessor));
    }

    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var
            principal = await base
                .CreateAsync(
                    user); // adds all `Options.ClaimsIdentity.RoleClaimType -> Role Claims` automatically + `Options.ClaimsIdentity.UserIdClaimType -> userId` & `Options.ClaimsIdentity.UserNameClaimType -> userName`
        AddCustomClaims(user, principal);
        return principal;
    }

    private static void AddCustomClaims(User user, IPrincipal principal)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        ((ClaimsIdentity)principal.Identity)?.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
            new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new Claim(PhotoFileName, user.PhotoFileName ?? string.Empty, ClaimValueTypes.String)
        });
    }
}