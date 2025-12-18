using MVC_Demo.Models;

namespace MVC_Demo.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new();

        public List<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(c => c.Id == id);

        public void Add(Product product)
        {
            product.Id = _products.Count > 0 ? _products.Max(c => c.Id) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
            }
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null) _products.Remove(existing);
        }
    }
}
