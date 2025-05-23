using Entities;

namespace DevopsLesson3.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetProducts(int count) => _productRepository.GetProducts(count);
        public Product? GetProductById(int productId) => _productRepository.GetProductById(productId);
        public Product Add(Product product) => _productRepository.Add(product);
        public Product Update(Product product) => _productRepository.Update(product);
        public bool Delete(int id) => _productRepository.Delete(id);
    }
}
