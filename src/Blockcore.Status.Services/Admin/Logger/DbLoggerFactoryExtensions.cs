﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlockcoreStatus.Services.Admin.Logger;

public static class DbLoggerFactoryExtensions
{
    public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
        return builder;
    }
}