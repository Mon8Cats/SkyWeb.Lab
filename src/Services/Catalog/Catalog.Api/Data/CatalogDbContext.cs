using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Entities;
using Catalog.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogDbContext : ICatalogDbContext
    {
        public CatalogDbContext(IOptions<MongoDbSettings> mongoDbOptions)
        {
            var mongoDbSettings = mongoDbOptions.Value;
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);

            Products = database.GetCollection<Product>(mongoDbSettings.CollectionName);

            CatalogDbContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}