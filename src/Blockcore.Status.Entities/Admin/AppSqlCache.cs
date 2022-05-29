﻿namespace BlockcoreStatus.Entities.Admin;

public class AppSqlCache
{
    public string Id { get; set; }
    public byte[] Value { get; set; }
    public DateTimeOffset ExpiresAtTime { get; set; }
    public long? SlidingExpirationInSeconds { get; set; }
    public DateTimeOffset? AbsoluteExpiration { get; set; }
}