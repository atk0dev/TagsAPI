namespace Persistence.Settings
{
    public interface ITagsDatabaseSettings
    {
        string TagsCollectionName { get; set; }
       
        string ConnectionString { get; set; }
        
        string DatabaseName { get; set; }
    }
}