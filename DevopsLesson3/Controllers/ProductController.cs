using DevopsLesson3.Services;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevopsLesson3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProductById(int id)
        {
            var data = _service.GetProductById(id);
            return Ok(data);
        }  
        [HttpGet("Top")]
        public ActionResult GetProductTop(int count)
        {
            var data = _service.GetProducts(count);
            return Ok(data);
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            var prod = _service.GetProducts();
            product.Id = prod.Count() > 0 ? prod.Max(a => a.Id) + 1 : 1;
            _service.Add(product);
            return product;
        }     
        [HttpPut]
        public ActionResult<Product> Put(int id,[FromBody] Product product)
        {
            var result = _service.Update(product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
