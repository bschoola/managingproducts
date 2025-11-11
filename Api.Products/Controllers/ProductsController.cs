using Domain.Products.Contracts;
using Domain.Products.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Products.Controllers
{
    /// <summary>
    /// Controller for managing product operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            return Ok(await _productService.GetProducts());
        }

        /// <summary>
        /// Gets a product by identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>The product with the specified identifier.</returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            return Ok(await _productService.GetById(productId));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product data transfer object.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _productService.CreateProduct(productDto));
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="productDto">The product data transfer object.</param>
        /// <returns>The updated product.</returns>
        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(int productId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _productService.UpdateProduct(productId, productDto));
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>No content.</returns>
        [HttpDelete("productId")]
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.Delete(productId);
            return NoContent();
        }
    }
}
