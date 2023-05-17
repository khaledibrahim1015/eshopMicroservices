using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        // Get all Products 
        Task<IEnumerable<Product>> GetProducts();


        // Get all products by name 
        Task<IEnumerable<Product>> GetProductsByName(string productName);

        // Get all product in category 
        Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);

        // Get Product By Id 
        Task<Product> GetProductById(string id);

        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);







    }
}
