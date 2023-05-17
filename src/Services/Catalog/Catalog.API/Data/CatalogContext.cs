using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration)
        {
            // Setup connection with Mongodb (DockerContainer )
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            // Get DataBase Name 
            var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            // Get Collection Name and assign it to prop

            Products = db.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));


            // Seed Data In Database 
            CatalogContextSeed.SeedData(Products);
        }



    }

    
}
