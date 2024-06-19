using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Context;
using APIBrechoRFCC.Infrastructure.Interface;
using APIBrechoRFCC.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;


namespace BrechoRFCC.Infrastructure.Repository
{
    public class ProductVariantRepository : ICRUDRepository<ProductVariant>
    {
        private readonly ECommerceDbContext _context;

        public ProductVariantRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<ProductVariant> Create(ProductVariant input)
        {
            var product = await _context.Products.FindAsync(input.ProductId);
            if (product == null) { throw new ProductNotFoundException(input.ProductId); }

            await _context.ProductVariants.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<List<ProductVariant>> GetAll()
        {
            return await _context.ProductVariants.AsNoTracking().ToListAsync();
        }

        public async Task<ProductVariant> GetById(int id)
        {
            var variant = await _context.ProductVariants.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            if(variant == null) { throw new ProductVariantNotFoundException(id); }
            return variant;
        }

        public async Task<ProductVariant> Update(ProductVariant product)
        {
            _context.ProductVariants.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(int id)
        {
            var variant = await GetById(id);
            if (variant == null) throw new ProductVariantNotFoundException(id);

            _context.ProductVariants.Remove(variant);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
