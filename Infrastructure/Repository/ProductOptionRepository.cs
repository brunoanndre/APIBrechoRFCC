using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Context;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace BrechoRFCC.Infrastructure.Repository
{
    public class ProductOptionRepository : ICRUDRepository<ProductOption>
    {
        private readonly ECommerceDbContext _context;

        public ProductOptionRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<ProductOption> Create(ProductOption model)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == model.ProductId);
            if (product == null) { throw new ProductNotFoundException(model.ProductId); }
            await _context.ProductOptions.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<ProductOption>> GetAll()
        {
            return await _context.ProductOptions.AsNoTracking().ToListAsync();
        }

        public async Task<ProductOption> GetById(int id)
        {
            var productOption = await _context.ProductOptions.AsNoTracking().SingleOrDefaultAsync(po => po.Id == id);
            
            if (productOption == null) throw new ProductOptionNotFoundException(id);

            return productOption;
        }

        public async Task<ProductOption>? Update(ProductOption model)
        {
            //Verificar se o Product existe
            var product = await _context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id == model.ProductId);
            if (product == null) throw new ProductNotFoundException(model.ProductId);
            _context.ProductOptions.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var option = await GetById(id);
            if (option == null) throw new ProductOptionNotFoundException(id);

            _context.ProductOptions.Remove(option);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
