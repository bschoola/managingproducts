using Domain.Products.Dto;
using Infrastructure.Products.Entities;

namespace Domain.Products.Contracts
{
    /// <summary>
    /// Defines the contract for product-related business operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products from the system.
        /// </summary>
        /// <returns>list of all products.</returns>
        Task<List<Product>> GetProducts();

        /// <summary>
        /// Retrieves a specific product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to retrieve.</param>
        /// <returns>product with the specified identifier.</returns>
        Task<Product> GetById(int id);

        /// <summary>
        /// Creates a new product in the system.
        /// </summary>
        /// <param name="product">The product data transfer object containing the details of the product to create.</param>
        /// <returns>created product.</returns>
        Task<Product> CreateProduct(ProductDto product);

        /// <summary>
        /// Updates an existing product with new information.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to update.</param>
        /// <param name="product">The product data transfer object containing the updated details.</param>
        /// <returns>updated product.</returns>
        Task<Product> UpdateProduct(int productId, ProductDto product);

        /// <summary>
        /// Deletes a product from the system.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to delete.</param>
        /// <returns></returns>
        Task Delete(int productId);
    }
}
