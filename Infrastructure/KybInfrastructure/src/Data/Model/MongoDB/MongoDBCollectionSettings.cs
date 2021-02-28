using MongoDB.Driver;

namespace KybInfrastructure.Data
{
    public class MongoDBCollectionSettings : IRepositorySettings<MongoDBSettings>
    {
        public MongoDBSettings DatabaseSettings { get; set; }
        public string CollectionName { get; set; }
        public CreateCollectionOptions CreateCollectionOptions { get; set; }
    }
}