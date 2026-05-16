using Api.Products.Models;
using Asp.Versioning;
using Domain.Products.Contracts;
using Domain.Products.Dto;
using Infrastructure.Products.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Products.Controllers
{
    /// <summary>
    /// Endpoints for product management.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Returns all registered products.
        /// </summary>
        /// <returns>List of all products.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Products()
        {
            return Ok(await _productService.GetProducts());
        }

        /// <summary>
        /// Returns a product by its identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>The product matching the given ID.</returns>
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int productId)
        {
            return Ok(await _productService.GetById(productId));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">Product data to be created.</param>
        /// <returns>The created product with its generated ID.</returns>
        /// <remarks>
        /// Request body example:
        ///
        ///     POST /api/products
        ///     {
        ///         "name": "Notebook",
        ///         "description": "High-performance laptop",
        ///         "price": 4999.90
        ///     }
        ///
        /// Validation rules:
        /// - `name` is required and cannot be empty (max 100 characters)
        /// - `price` must be greater than or equal to zero
        /// - `description` is optional (max 150 characters)
        /// </remarks>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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
        /// <param name="productId">The unique identifier of the product to update.</param>
        /// <param name="productDto">Updated product data.</param>
        /// <returns>The product with updated data.</returns>
        /// <remarks>
        /// Request body example:
        ///
        ///     PUT /api/products/1
        ///     {
        ///         "name": "Notebook Pro",
        ///         "description": "Professional laptop with SSD",
        ///         "price": 6499.90
        ///     }
        ///
        /// Returns **400** if the product is not found or if the data is invalid.
        /// </remarks>
        [HttpPut("{productId}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int productId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _productService.UpdateProduct(productId, productDto));
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to delete.</param>
        /// <returns>No content on success.</returns>
        /// <remarks>
        /// Returns **400** if the product is not found.
        /// The deletion is permanent and cannot be undone.
        /// </remarks>
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int productId)
        {
            await _productService.Delete(productId);
            return NoContent();
        }
    }
}
