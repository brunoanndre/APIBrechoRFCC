﻿using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Context;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace BrechoRFCC.Infrastructure.Repository
{
    public class ProductRepository : ICRUDRepository<Product>
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Create(Product product)
        {
            var category = await _context.Categories.FindAsync(product.CategoryId);
            if (category == null) throw new CategoryNotFoundException(product.CategoryId);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .Include(p => p.Options)
                .Include(p => p.Variants)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Variants)
                .Include(p => p.Options).SingleOrDefaultAsync(p => p.Id == id);

            if(product == null) throw new ProductNotFoundException(id);
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
