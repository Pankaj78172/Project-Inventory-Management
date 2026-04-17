using InventoryManagement.DTO;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        
            private readonly IProductService _productService;


            public ProductsController (IProductService productService)
            {
                _productService = productService;
            }


            [HttpGet]
            public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllProduct()
            {
                var product = await _productService.GetAllProductsAsync();
                return Ok(product);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ProductReadDto>> GetProductById(int id)
            {
                var product = await _productService.GetProductByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }

            [HttpPost]
            public async Task<ActionResult<ProductReadDto>> CreateProduct(ProductCreateDTO dto)
            {
                var createdProduct = await _productService.CreateProductAsync(dto);

                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = createdProduct.Id },
                    createdProduct
                );
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateProduct(int id, ProductUpdateDto dto)
            {
                var updated = await _productService.UpdateProductAsync(id, dto);

                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteProduct(int id)
            {
                var deleted = await _productService.DeleteProductAsync(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }

            [HttpGet("category/{category}")]
            public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProductsByCategory(string category)
            {
                var products = await _productService.GetProductsByCategoryAsync(category);
                return Ok(products);
            }

            [HttpGet("low-stock")]
            public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetLowStockProducts()
            {
                var products = await _productService.GetLowStockProductsAsync();
                return Ok(products);
            }

            [HttpGet("search")]
            public async Task<ActionResult<IEnumerable<ProductReadDto>>> SearchProducts([FromQuery] string name)
            {
                var products = await _productService.SearchProductsAsync(name);
                return Ok(products);
            }



        }

    
}
