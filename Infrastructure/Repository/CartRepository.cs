using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Context;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace BrechoRFCC.Infrastructure.Repository
{
    public class CartRepository : ICRUDRepository<Cart>
    {
        private readonly ECommerceDbContext _context;

        public CartRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> Create(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            
            return cart;
        }

        public async Task<Cart> Addline(int cartId, int productVariantId)
        {
            var cart = await GetById(cartId);
            if (cart == null) throw new CartNotFoundException(cartId);

            var productVariant = await _context.ProductVariants.SingleOrDefaultAsync(p => p.Id == productVariantId);
            if (productVariant == null) throw new ProductVariantNotFoundException(productVariantId);

            CartLine cartLine = new CartLine
            {
                CartId = cartId,
                Cart = cart,
                Variant = productVariant,
                VariantId = productVariantId,
                Quantity = 1,
                Total = productVariant.SellingPrice,
            };
            cart.CartLines.Add(cartLine);
            //Adiciona o cartline no banco
            await _context.CartLines.AddAsync(cartLine);
            
            //Atualiza o cart com o item adicionado
             _context.Carts.Update(cart);
            await _context.SaveChangesAsync();

            return cart;
        }
        
        public async Task<Cart> UpdateLine(int cartId,CartLineInputDTO cartline)
        {
            var cart = await GetById(cartId);
            if (cart == null) throw new CartNotFoundException(cartId);

            var lineToUpdate = cart.CartLines.FirstOrDefault(c => c.Id == cartline.Id);
            if(lineToUpdate == null) throw new CartlineNotFoundException(cartline.Id);

            lineToUpdate.VariantId = cartline.ProductVariantId;
            lineToUpdate.Quantity = cartline.Quantity;
            lineToUpdate.Total = cartline.Total;
            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<List<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetById(int id)
        {
            var cart = await _context.Carts.SingleOrDefaultAsync(c => c.Id == id);
            if (cart == null) throw new CartNotFoundException(id);
            return cart;
        }

        public async Task<Cart> Update(Cart input)
        {
            if (await GetById(input.Id) == null) throw new CartNotFoundException(input.Id);

            _context.Carts.Update(input);
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<bool> Delete(int id)
        {
            Cart cart = await GetById(id);
            if(cart == null) throw new CartNotFoundException(id);

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
