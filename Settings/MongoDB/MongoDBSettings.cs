namespace JwtDotNet7.Settings.MongoDB
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public const String SectionName = "MongoDBConnectionSettings";
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
    }
}