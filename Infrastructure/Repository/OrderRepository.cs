using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace APIBrechoRFCC.Infrastructure.Repository
{
    public class OrderRepository
    {
        private readonly ECommerceDbContext _context;

        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task Create(Order order)
        {
            //order.CalculateTotal();
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.AsNoTracking()
                .Include(x => x.Items)
                .ToListAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            var order = await _context.Orders
                .Include(x=> x.Items)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (order == null) { throw new OrderNotFoundException(id); }
            return order;
        }
    }
}
