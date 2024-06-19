using AutoMapper;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Core.Entities;
using BrechoRFCC.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using APIBrechoRFCC.Core.Exceptions;

namespace APIBrechoRFCC.API.Controllers
{
    [Route("v1/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartRepository _repository;
        private readonly IMapper _mapper;

        public CartController(CartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET - v1/carts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CartOutputDTO>> GetById(int id)
        {
            if (!String.IsNullOrEmpty(id.ToString())) return BadRequest("Id do carrinho inválido");
            try
            {
                Cart cart = await _repository.GetById(id);

                CartOutputDTO cartDTO = _mapper.Map<CartOutputDTO>(cart);
                return Ok(cartDTO);
            }
            catch (CartNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

        //POST - v1/carts
        [HttpPost]
        public async Task<ActionResult<CartOutputDTO>> Create(CartInputDTO input)
        {
            Cart cart = _mapper.Map<Cart>(input);

            var cartDB = await _repository.Create(cart);

            var cartDTO = _mapper.Map<CartOutputDTO>(cartDB);

            return CreatedAtAction("GetById", new { Id = cartDTO.Id }, cartDTO);
        }

        //POST v1/carts/{id}/lines
        [HttpPost("{cartId}/lines")]
        public async Task<ActionResult<CartOutputDTO>> AddCartLine(int cartId, [FromQuery] int productVariantId)
        {
            try
            {
                var cart = await _repository.Addline(cartId, productVariantId);

                var cartOutputDTO = _mapper.Map<CartOutputDTO>(cart);

                return Ok(cartOutputDTO);
            }catch(CartNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        //Patch - v1/carts/{cartId}/lines
        [HttpPatch("{cartId}/lines")]
        public async Task<ActionResult<CartOutputDTO>> UpdateCartLine(int cartId, [FromBody] CartLineInputDTO cartline)
        {
            try
            {
                var cart = await _repository.UpdateLine(cartId, cartline);

                var cartDTO = _mapper.Map<CartOutputDTO>(cart);

                return Ok(cartDTO);
            }
            catch (CartNotFoundException e)
            {
                return NotFound(e.Message);
            }catch(CartlineNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<CartOutputDTO>>> GetAll()
        {
            List<Cart> carts = await _repository.GetAll();
            if(carts == null || carts.Count == 0) { return NotFound("Nenhum carrinho encontrado."); }

            List<CartOutputDTO> cartsDTO = _mapper.Map<List<CartOutputDTO>>(carts);

            return Ok(cartsDTO);
        }


    }
}
