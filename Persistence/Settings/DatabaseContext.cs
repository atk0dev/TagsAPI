namespace Persistence.Settings
{
    public class DatabaseContext : IDatabaseContext
    {
        public string ConnectionString { get; set; }
      
        public string DatabaseName { get; set; }
    }
}
