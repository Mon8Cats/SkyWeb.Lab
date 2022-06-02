using Catalog.Api.Settings;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Catalog.Api.Extensions
{
    public static class MongoDbExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            //ServiceSettings serviceSettings;
            

            //BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); 
            //BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); 

            services.AddSingleton(serviceProvider => 
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                //var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                // dotnet add package Microsoft.Extensions.Configuration.Binder
                //var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Value;
                //var mongoDbSettigns = configuration.GetSection(nameof(MongoDbSettings)).Value;
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(mongoDbSettings.DatabaseName);

            });

            return services;
        }
    }
}