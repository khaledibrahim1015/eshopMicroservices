using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Product>> GetProducts()
        {
            var filter = Builders<Product>.Filter.Empty;

            return await _context.Products.
                                Find(filter).
                                    ToListAsync();

        }





        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            var filter = Builders<Product>
                                        .Filter
                                           . Eq(p => p.Category, categoryName);


            return await _context.Products
                                        .Find(filter)
                                            .ToListAsync();


        }

        public async Task<IEnumerable<Product>> GetProductsByName(string productName)
        {
            var filter = new FilterDefinitionBuilder<Product>()
                                                    .Eq(p => p.Name, productName);

            return await _context.Products
                                    .Find(filter)
                                        .ToListAsync();


        }

        public async Task<Product> GetProductById(string id)
        {

            return await _context.Products
                                    .Find(p => p.Id == id)
                                          .FirstOrDefaultAsync();

        }

        public async Task CreateProduct(Product product)
            =>  await _context.Products.InsertOneAsync(product);


       

        public async Task<bool> UpdateProduct(Product product)
        {
           
            var filter  =  Builders<Product>
                                        .Filter
                                            .Eq(p => p.Id ,product.Id);

           var updateResult= await _context.Products.ReplaceOneAsync(filter: filter, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;

        }



        public async Task<bool> DeleteProduct(string id)
        {
            
            var filter  =  Builders<Product>.Filter.Eq(p=>p.Id , id);

            var DeleteResult= await _context.Products
                                           .DeleteOneAsync(filter);

            if (DeleteResult.IsAcknowledged && DeleteResult.DeletedCount > 0)
                return true;
            return false;

        }


    }
}
