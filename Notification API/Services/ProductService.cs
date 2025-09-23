using Notification_API.Models;

namespace Notification_API.Services
{
    public class ProductService: IProductService
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public Product AddProduct(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            return product;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"Product with Id {id} not found.");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product UpdateProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                throw new InvalidOperationException($"Product with Id {product.Id} not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return existingProduct;
        }

        public bool DeleteProduct(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Id == id);
            if (productToRemove == null)
            {
                return false;
            }

            _products.Remove(productToRemove);
            return true;
        }
    }
}
