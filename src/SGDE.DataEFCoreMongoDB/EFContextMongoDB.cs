namespace SGDE.DataEFCoreMongoDB
{  
    #region Using

    using MongoDB.Driver;
    using MongoDB.Bson.Serialization;
    using System.Threading.Tasks;
    using System.Threading;
    using Domain.Entities;
    using Domain.Helpers;

    #endregion

    public class EFContextMongoDB
    {
        public IMongoDatabase Database { get; }
        private readonly IMongoClient _client;
        public IClientSessionHandle Session { get; private set; }

        public IMongoCollection<Order> Orders => Database.GetCollection<Order>("orders");

        public EFContextMongoDB(InfrastructureAppSettings infrastructure)
        {
            _client = new MongoClient(infrastructure.ConnectionString);
            if (_client != null)
                Database = _client.GetDatabase(infrastructure.DatabaseName);

            ClassMapping();
        }

        private static void ClassMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Order))) { BsonClassMap.RegisterClassMap<Order>(); }
        }

        public async Task<IClientSessionHandle> StartSession(CancellationToken cancellactionToken = default(CancellationToken))
        {
            var session = await _client.StartSessionAsync(cancellationToken: cancellactionToken);
            Session = session;
            return session;
        }
    }
}
