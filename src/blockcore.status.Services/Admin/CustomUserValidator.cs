using blockcore.status.Common.GuardToolkit;
using blockcore.status.Entities.Admin;
using blockcore.status.ViewModels.Admin.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace blockcore.status.Services.Admin;

public class CustomUserValidator : UserValidator<User>
{
    private readonly ISet<string> _emailsBanList;

    public CustomUserValidator(
        IdentityErrorDescriber errors, // How to use CustomIdentityErrorDescriber
        IOptionsSnapshot<SiteSettings> configurationRoot
    ) : base(errors)
    {
        if (configurationRoot == null)
        {
            throw new ArgumentNullException(nameof(configurationRoot));
        }

        _emailsBanList = new HashSet<string>(configurationRoot.Value.EmailsBanList, StringComparer.OrdinalIgnoreCase);

        if (!_emailsBanList.Any())
        {
            throw new InvalidOperationException("Please fill the emails ban list in the appsettings.json file.");
        }
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        // First use the built-in validator
        var result = await base.ValidateAsync(manager, user);
        var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

        // Extending the built-in validator
        ValidateEmail(user, errors);
        ValidateUserName(user, errors);

        return !errors.Any() ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }

    private void ValidateEmail(User user, List<IdentityError> errors)
    {
        var userEmail = user?.Email;
        if (string.IsNullOrWhiteSpace(userEmail))
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                errors.Add(new IdentityError
                {
                    Code = "EmailIsNotSet",
                    Description = "Please complete email information."
                });
            }

            return; // base.ValidateAsync() will cover this case
        }

        if (_emailsBanList.Any(email => userEmail.EndsWith(email, StringComparison.OrdinalIgnoreCase)))
        {
            errors.Add(new IdentityError
            {
                Code = "BadEmailDomainError",
                Description = "Please enter a valid email provider."
            });
        }
    }

    private static void ValidateUserName(User user, List<IdentityError> errors)
    {
        var userName = user?.UserName;
        if (string.IsNullOrWhiteSpace(userName))
        {
            errors.Add(new IdentityError
            {
                Code = "UserIsNotSet",
                Description = "Please enter a username."
            });

            return; // base.ValidateAsync() will cover this case
        }

        if (userName.IsNumeric() || userName.ContainsNumber())
        {
            errors.Add(new IdentityError
            {
                Code = "BadUserNameError",
                Description = "The username entered cannot contain numbers."
            });
        }

        if (userName.HasConsecutiveChars())
        {
            errors.Add(new IdentityError
            {
                Code = "BadUserNameError",
                Description = "The username entered is not valid."
            });
        }
    }
}