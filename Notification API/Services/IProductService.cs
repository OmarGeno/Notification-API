using Notification_API.Models;

namespace Notification_API.Services
{
    public interface IProductService
    {
        Product AddProduct(Product product);
        Product GetProductById(int id);
        IEnumerable<Product> GetAllProducts();
        Product UpdateProduct(Product product);
        bool DeleteProduct(int id);
    }
}
