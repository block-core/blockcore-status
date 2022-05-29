﻿using blockcore.status.DataLayer.Context;
using blockcore.status.Entities.AuditableEntity;
using blockcore.status.Entities.Admin;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.ViewModels.Admin.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace blockcore.status.Services.Admin;

public class UsedPasswordsService : IUsedPasswordsService
{
    private readonly int _changePasswordReminderDays;
    private readonly int _notAllowedPreviouslyUsedPasswords;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _uow;
    private readonly DbSet<UserUsedPassword> _userUsedPasswords;

    public UsedPasswordsService(
        IUnitOfWork uow,
        IPasswordHasher<User> passwordHasher,
        IOptionsSnapshot<SiteSettings> configurationRoot)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

        _userUsedPasswords = _uow.Set<UserUsedPassword>();
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        if (configurationRoot == null)
        {
            throw new ArgumentNullException(nameof(configurationRoot));
        }

        var configurationRootValue = configurationRoot.Value;
        if (configurationRootValue == null)
        {
            throw new InvalidOperationException($"{nameof(configurationRootValue)} is null");
        }

        _notAllowedPreviouslyUsedPasswords = configurationRootValue.NotAllowedPreviouslyUsedPasswords;
        _changePasswordReminderDays = configurationRootValue.ChangePasswordReminderDays;
    }

    public async Task AddToUsedPasswordsListAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _userUsedPasswords.AddAsync(new UserUsedPassword
        {
            UserId = user.Id,
            HashedPassword = user.PasswordHash
        });
        await _uow.SaveChangesAsync();
    }

    public async Task<DateTime?> GetLastUserPasswordChangeDateAsync(int userId)
    {
        var lastPasswordHistory =
            await _userUsedPasswords //.AsNoTracking() --> removes shadow properties
                .OrderByDescending(userUsedPassword => userUsedPassword.Id)
                .FirstOrDefaultAsync(userUsedPassword => userUsedPassword.UserId == userId);
        if (lastPasswordHistory == null)
        {
            return null;
        }

        var createdDateValue =
            _uow.GetShadowPropertyValue(lastPasswordHistory, AuditableShadowProperties.CreatedDateTime);
        return createdDateValue == null ? null : DateTime.SpecifyKind((DateTime)createdDateValue, DateTimeKind.Utc);
    }

    public async Task<bool> IsLastUserPasswordTooOldAsync(int userId)
    {
        var createdDateTime = await GetLastUserPasswordChangeDateAsync(userId);
        if (createdDateTime == null)
        {
            return false;
        }

        return createdDateTime.Value.AddDays(_changePasswordReminderDays) < DateTime.UtcNow;
    }

    /// <summary>
    ///     This method will be used by CustomPasswordValidator automatically,
    ///     every time a user wants to change his/her info.
    /// </summary>
    public async Task<bool> IsPreviouslyUsedPasswordAsync(User user, string newPassword)
    {
        if (user is null || user.Id == 0)
        {
            // A new user wants to register at our site
            return false;
        }

        var userId = user.Id;
        var usedPasswords = await _userUsedPasswords
            .AsNoTracking()
            .Where(userUsedPassword => userUsedPassword.UserId == userId)
            .OrderByDescending(userUsedPassword => userUsedPassword.Id)
            .Select(userUsedPassword => userUsedPassword.HashedPassword)
            .Take(_notAllowedPreviouslyUsedPasswords)
            .ToListAsync();
        return usedPasswords.Any(hashedPassword =>
            _passwordHasher.VerifyHashedPassword(user, hashedPassword, newPassword) !=
            PasswordVerificationResult.Failed);
    }
}