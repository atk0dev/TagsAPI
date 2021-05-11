namespace Persistence.Settings
{
    public interface IDatabaseContext
    {
        string ConnectionString { get; set; }
        
        string DatabaseName { get; set; }
    }
}