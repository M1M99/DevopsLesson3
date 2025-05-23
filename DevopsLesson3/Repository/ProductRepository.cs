using DevopsLesson3.Data;
using Entities;

namespace DevopsLesson3.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product Add(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var item = _dbContext.Products.FirstOrDefault(a => a.Id == id);
            if (item is not null)
            {
                _dbContext.Products.Remove(item) ;
                return _dbContext.SaveChanges() > 0;

            }
            return false;
        }

        public Product? GetProductById(int productId)
        {
            return _dbContext.Products.FirstOrDefault(a => a.Id == productId);
        }

        public IEnumerable<Product> GetProducts(int top = 0)
        {
            return top == 0 ? _dbContext.Products : (_dbContext.Products.Count() > 0 ? _dbContext.Products.Take(top) : new List<Product>());
        }

        public Product Update(Product product)
        {
            var item = _dbContext.Products.FirstOrDefault(a => a.Id == product.Id);
            if (item is not null)
            {
                item.Name = product.Name;
                item.Price = product.Price;

                _dbContext.SaveChanges();

                return item;
            }
            return null;
        }

    }
}
