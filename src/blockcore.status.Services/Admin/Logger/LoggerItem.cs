using blockcore.status.Entities.AuditableEntity;
using blockcore.status.Entities.Admin;

namespace blockcore.status.Services.Admin.Logger;

public class LoggerItem
{
    public AppShadowProperties Props { set; get; }
    public AppLogItem AppLogItem { set; get; }
}