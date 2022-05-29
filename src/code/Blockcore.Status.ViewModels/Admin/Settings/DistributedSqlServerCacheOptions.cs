namespace blockcore.status.ViewModels.Admin.Settings;

public class DistributedSqlServerCacheOptions
{
    public string ConnectionString { set; get; }
    public string TableName { set; get; }
    public string SchemaName { set; get; }
}