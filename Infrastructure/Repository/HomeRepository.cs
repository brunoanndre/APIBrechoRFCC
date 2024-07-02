using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace APIBrechoRFCC.Infrastructure.Repository
{
    public class HomeRepository
    {
        private readonly ECommerceDbContext _context;

        public HomeRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task CreateHomeBanner(HomeBanner banner)
        {
            await _context.HomeBanners.AddAsync(banner);
            await _context.SaveChangesAsync();
        }

        public async Task CreateHomeSection(HomeSection section)
        {
            await _context.HomeSections.AddAsync(section);
            await _context.SaveChangesAsync();
        }
        public async Task<List<HomeBanner>> GetBanners()
        {
            return await _context.HomeBanners.ToListAsync();
        } 

        public async Task<List<HomeSection>> GetSections()
        {
            return await _context.HomeSections.ToListAsync();
        }
    }
}
