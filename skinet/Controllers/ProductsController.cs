using Microsoft.AspNetCore.Mvc;
using skinet.Data;
using skinet.Models;
using skinet.Repositories;

namespace skinet.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> getProductsAsynch()
        {
            var allProducts = await _productRepository.GetAllAsynch();
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProduct(int id)
        {
            var product = await _productRepository.GetByIdAsynch(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
    }
}
