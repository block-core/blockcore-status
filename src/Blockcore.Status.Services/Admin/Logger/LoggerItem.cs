using BlockcoreStatus.Entities.AuditableEntity;
using BlockcoreStatus.Entities.Admin;

namespace BlockcoreStatus.Services.Admin.Logger;

public class LoggerItem
{
    public AppShadowProperties Props { set; get; }
    public AppLogItem AppLogItem { set; get; }
}