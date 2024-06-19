using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Context;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace BrechoRFCC.Infrastructure.Repository
{
    public class CategoryRepository : ICRUDRepository<Category>
    {
        private readonly ECommerceDbContext _context;

        public CategoryRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.AsNoTracking().Where(c => c.IsActive).ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {    
            var category = await _context.Categories.Include(c => c.Products).SingleOrDefaultAsync(c => c.Id == id);
            if(category == null)  throw new CategoryNotFoundException(id);

            return category;
        }

        public async Task<Category> Update(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<bool> Delete(int id)
        {
            var category = await GetById(id);
            category.Delete();
            var deleted = await Update(category);
            return true;
        }

    }
}
