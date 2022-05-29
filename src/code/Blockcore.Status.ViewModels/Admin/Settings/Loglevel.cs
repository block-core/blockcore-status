using MsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace blockcore.status.ViewModels.Admin.Settings;

public class Loglevel
{
    public MsLogLevel Default { get; set; }
    public MsLogLevel System { get; set; }
    public MsLogLevel Microsoft { get; set; }
}