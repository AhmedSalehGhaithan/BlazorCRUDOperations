using BlazorCRUDOperations.Data;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using SharedLibrary.ProductRepository;
using System;

namespace BlazorCRUDOperations.Implemention
{
    public class ProductRepository(AppDbContext _appDbContext) : IProductRepository
    {
        public async Task<Product> AddProductAsync(Product model)
        {
            if (model is null) return null!;
            var chk = await _appDbContext.Products.Where(_ => _.Name.ToLower().Equals(model.Name.ToLower())).FirstOrDefaultAsync();
            if (chk is not null) return null!;

            var newDataAdded = _appDbContext.Products.Add(model).Entity;
            await _appDbContext.SaveChangesAsync();
            return newDataAdded;
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(_ => _.Id == productId);
            if (product is null) return null!;
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }
        
        public async Task<List<Product>> GetAllProductsAsync() => await _appDbContext.Products.ToListAsync();
        
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(_ => _.Id == productId);
            if (product is null) return null!;
            return product;
        }

        public async Task<Product> UpdateProductAsync(Product model)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(_ => _.Id == model.Id);
            if (product is null) return null!;
            product.Name = model.Name;
            product.Quantity = model.Quantity;
            await _appDbContext.SaveChangesAsync();
            return product;
        }
    }
}
