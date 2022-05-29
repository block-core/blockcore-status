using BlockcoreStatus.Entities.Admin;
using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlockcoreStatus.Services.Admin;

public class CustomPasswordValidator : PasswordValidator<User>
{
    private readonly ISet<string> _passwordsBanList;
    private readonly IUsedPasswordsService _usedPasswordsService;

    public CustomPasswordValidator(
        IdentityErrorDescriber errors, // How to use CustomIdentityErrorDescriber
        IOptionsSnapshot<SiteSettings> configurationRoot,
        IUsedPasswordsService usedPasswordsService) : base(errors)
    {
        _usedPasswordsService = usedPasswordsService ?? throw new ArgumentNullException(nameof(usedPasswordsService));
        if (configurationRoot == null)
        {
            throw new ArgumentNullException(nameof(configurationRoot));
        }

        _passwordsBanList =
            new HashSet<string>(configurationRoot.Value.PasswordsBanList, StringComparer.OrdinalIgnoreCase);

        if (!_passwordsBanList.Any())
        {
            throw new InvalidOperationException("Please fill the passwords ban list in the appsettings.json file.");
        }
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var errors = new List<IdentityError>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordIsNotSet",
                Description = "Please enter a password."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        if (string.IsNullOrWhiteSpace(user?.UserName))
        {
            errors.Add(new IdentityError
            {
                Code = "UserNameIsNotSet",
                Description = "Please enter a username."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        // First use the built-in validator
        var result = await base.ValidateAsync(manager, user, password);
        errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

        // Extending the built-in validator
        if (password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordContainsUserName",
                Description = "Password cannot contain part of username."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        if (!IsSafePasword(password))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordIsNotSafe",
                Description = "The password entered is easy to guess."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        if (await _usedPasswordsService.IsPreviouslyUsedPasswordAsync(user, password))
        {
            errors.Add(new IdentityError
            {
                Code = "IsPreviouslyUsedPassword",
                Description =
                    "Please choose another password. This password has been used by you before and is duplicate."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        return !errors.Any() ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }

    private static bool AreAllCharsEqual(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return false;
        }

        data = data.ToLowerInvariant();
        var firstElement = data.ElementAt(0);
        var euqalCharsLen = data.ToCharArray().Count(x => x == firstElement);
        if (euqalCharsLen == data.Length)
        {
            return true;
        }

        return false;
    }

    private bool IsSafePasword(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return false;
        }

        if (data.Length < 5)
        {
            return false;
        }

        if (_passwordsBanList.Contains(data.ToLowerInvariant()))
        {
            return false;
        }

        if (AreAllCharsEqual(data))
        {
            return false;
        }

        return true;
    }
}