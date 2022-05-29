﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;

namespace BlockcoreStatus.Services.Admin;

public class DistributedCacheTicketStore : ITicketStore
{
    private const string KeyPrefix = "AuthSessionStore-";
    private readonly IDistributedCache _cache;
    private readonly IDataSerializer<AuthenticationTicket> _ticketSerializer = TicketSerializer.Default;

    public DistributedCacheTicketStore(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<string> StoreAsync(AuthenticationTicket ticket)
    {
        var key = $"{KeyPrefix}{Guid.NewGuid().ToString("N")}";
        await RenewAsync(key, ticket);
        return key;
    }

    public Task RenewAsync(string key, AuthenticationTicket ticket)
    {
        if (ticket == null)
        {
            throw new ArgumentNullException(nameof(ticket));
        }
        // NOTE: Using `services.enableImmediateLogout();` will cause this method to be called per each request.

        var options = new DistributedCacheEntryOptions();

        var expiresUtc = ticket.Properties.ExpiresUtc;
        if (expiresUtc.HasValue)
        {
            options.SetAbsoluteExpiration(expiresUtc.Value);
        }

        if (ticket.Properties.AllowRefresh ?? false)
        {
            options.SetSlidingExpiration(TimeSpan.FromMinutes(30)); // TODO: configurable.
        }

        return _cache.SetAsync(key, _ticketSerializer.Serialize(ticket), options);
    }

    public async Task<AuthenticationTicket> RetrieveAsync(string key)
    {
        var value = await _cache.GetAsync(key);
        return value != null ? _ticketSerializer.Deserialize(value) : null;
    }

    public Task RemoveAsync(string key)
    {
        return _cache.RemoveAsync(key);
    }
}