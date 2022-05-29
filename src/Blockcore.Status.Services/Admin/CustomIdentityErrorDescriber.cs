using Microsoft.AspNetCore.Identity;

namespace BlockcoreStatus.Services.Admin;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError ConcurrencyFailure()
    {
        return new IdentityError
        {
            Code = nameof(ConcurrencyFailure),
            Description = "The current record has already been edited and your changes will overwrite it."
        };
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError
        {
            Code = nameof(DefaultError),
            Description = "An error has occurred."
        };
    }

    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = string.Format(CultureInfo.InvariantCulture, "Email '{0}' is currently in use.", email)
        };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = string.Format(CultureInfo.InvariantCulture, "The role '{0}' is currently in use.", role)
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = string.Format(CultureInfo.InvariantCulture, "Username '{0}' is currently in use.",
                userName)
        };
    }

    public override IdentityError InvalidEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = string.Format(CultureInfo.InvariantCulture, "Email '{0}' is not valid.", email)
        };
    }

    public override IdentityError InvalidRoleName(string role)
    {
        return new IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = string.Format(CultureInfo.InvariantCulture, "The role '{0}' is not valid.", role)
        };
    }

    public override IdentityError InvalidToken()
    {
        return new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = "Invalid token."
        };
    }

    public override IdentityError InvalidUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = string.Format(CultureInfo.InvariantCulture,
                "Username '{0}' is invalid and can only contain letters or numbers.",
                userName)
        };
    }

    public override IdentityError LoginAlreadyAssociated()
    {
        return new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = "This user has already been added."
        };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = "Invalid password."
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "Password must have at least one digit between 0 and 9."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "Password must contain at least one lowercase English letter."
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "Password must contain at least one letter outside the English alphabet as well as numbers."
        };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = "Password must contain at least {0} different letters."
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "Password must contain at least one capital letter in English."
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = string.Format(CultureInfo.InvariantCulture, "Password must be at least {0} characters long.", length)
        };
    }

    public override IdentityError RecoveryCodeRedemptionFailed()
    {
        return new IdentityError
        {
            Code = nameof(RecoveryCodeRedemptionFailed),
            Description = "Recovery failed."
        };
    }

    public override IdentityError UserAlreadyHasPassword()
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = "User's password is already set."
        };
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyInRole),
            Description = string.Format(CultureInfo.InvariantCulture, "User currently has role '{0}'.", role)
        };
    }

    public override IdentityError UserLockoutNotEnabled()
    {
        return new IdentityError
        {
            Code = nameof(UserLockoutNotEnabled),
            Description = "Account lock is not set for this user."
        };
    }

    public override IdentityError UserNotInRole(string role)
    {
        return new IdentityError
        {
            Code = nameof(UserNotInRole),
            Description = "User does not have '{0}' role."
        };
    }
}