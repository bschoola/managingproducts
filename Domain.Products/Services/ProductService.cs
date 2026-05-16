using Domain.Products.Contracts;
using Domain.Products.Dto;
using Infrastructure.Products.Context;
using Infrastructure.Products.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products.Services
{
    public class ProductService : IProductService
    {
        private ProductsContext _context;
        public ProductService(ProductsContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProduct(ProductDto productDto)
        {
            // Use Mapper
            var product = new Product()
            {
                Description = productDto.Description,
                Name = productDto.Name,
                Price = productDto.Price
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(int productId, ProductDto productDto)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product does not exist");
            }
            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task Delete(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product does not exist");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
