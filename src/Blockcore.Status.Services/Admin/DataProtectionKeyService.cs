using System.Collections.ObjectModel;
using System.Xml.Linq;
using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities.Admin;
using Common.Web.Core;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlockcoreStatus.Services.Admin;

public class DataProtectionKeyService : IXmlRepository
{
    private readonly ILogger<DataProtectionKeyService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DataProtectionKeyService(IServiceProvider serviceProvider, ILogger<DataProtectionKeyService> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IReadOnlyCollection<XElement> GetAllElements()
    {
        return _serviceProvider.RunScopedService<IUnitOfWork, ReadOnlyCollection<XElement>>(context =>
        {
            var dataProtectionKeys = context.Set<AppDataProtectionKey>().AsNoTracking();
            var logger = _logger;
            return dataProtectionKeys.Select(key => TryParseKeyXml(key.XmlData, logger)).ToList().AsReadOnly();
        });
    }

    public void StoreElement(XElement element, string friendlyName)
    {
        // We need a separate context to call its SaveChanges several times,
        // without using the current request's context and changing its internal state.
        _serviceProvider.RunScopedService<IUnitOfWork>(context =>
        {
            var dataProtectionKeys = context.Set<AppDataProtectionKey>();
            var entity = dataProtectionKeys.SingleOrDefault(k => k.FriendlyName == friendlyName);
            if (entity != null)
            {
                entity.XmlData = element.ToString();
                dataProtectionKeys.Update(entity);
            }
            else
            {
                dataProtectionKeys.Add(new AppDataProtectionKey
                {
                    FriendlyName = friendlyName,
                    XmlData = element.ToString(SaveOptions.DisableFormatting)
                });
            }

            context.SaveChanges();
        });
    }

    private static XElement TryParseKeyXml(string xml, ILogger logger)
    {
        try
        {
            return XElement.Parse(xml);
        }
        catch (Exception e)
        {
            logger.LogWarningMessage($"An exception occurred while parsing the key xml '{xml}': {e}.");
            return null;
        }
    }
}