namespace CaveCore.Settings
{
    public class DbSettings : IDbSettings
    {
        public string UserCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }

        public string PostCollectionName {get; set;}
        public string DatabaseName { get; set; }
    }
    
    public interface IDbSettings
    {
        string UserCollectionName { get; set; }
        string CategoryCollectionName { get; set; }
        string PostCollectionName {get; set;}
        string DatabaseName { get; set; }
    }
}
