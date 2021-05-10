namespace Persistence.Settings
{
    public class TagsDatabaseSettings : ITagsDatabaseSettings
    {
        public string TagsCollectionName { get; set; }
       
        public string ConnectionString { get; set; }
      
        public string DatabaseName { get; set; }
    }
}
