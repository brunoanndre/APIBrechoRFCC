/*using AutoMapper;
using BrechoRFCC.Application.DTO;
using BrechoRFCC.Core.Entities;
using BrechoRFCC.Core.Exceptions;
using BrechoRFCC.Infrastructure.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BrechoRFCC.API.Controllers
{
    [Route("v1/orderItem")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICRUDRepository<OrderItem> _repository;

        public OrderItemController(IMapper mapper, ICRUDRepository<OrderItem> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemOutputDTO>> Create(OrderItemOutputDTO input)
        {
            try
            {
                if (input == null) { return BadRequest("Dados inválidos."); }

                var orderItem = _mapper.Map<OrderItem>(input);
                //var orderItemDb = await _repository.Create(orderItem);
                var OrderItemDTO = _mapper.Map<OrderItemOutputDTO>(await _repository.Create(orderItem));
                return CreatedAtAction("GetById", OrderItemDTO);
            }
            catch (OrderNotFoundException e)
            {
                return Ok(e.Message);
            }
        }

        // GET - v1/orderItem
        [HttpGet]
        public async Task<ActionResult<List<OrderItemOutputDTO>>> GetAll()
        {
            var orderItemsDTO = _mapper.Map<OrderItemOutputDTO>(await _repository.GetAll());
            if(orderItemsDTO == null) { return NotFound("Nenhum item encontrado."); }
            return Ok(orderItemsDTO);
        }

        // GET - v1/orderItem/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemOutputDTO>> GetById(int id)
        {
            if(id == 0) { return BadRequest("ID deve ser maior que 0."); }
            var item = await _repository.GetById(id);

            return Ok(item);
        }

        //PUT - v1/orderItem/{id}
        [HttpPut("id")]
        public async Task<ActionResult<OrderItemOutputDTO>> Update(int id,OrderItemInputDTO input)
        {
            if(id == 0) { return BadRequest("ID inválido"); }

            var orderItem = _mapper.Map<OrderItem>(input);
            orderItem.Id = id;
            await _repository.Update(orderItem);
            return Ok(input);
        }

        //DELETE - v1/orderItem/{id}
        [HttpPost("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id == 0)  return BadRequest("ID inválido.");

            await _repository.Delete(id);

            return Ok();
        }
        
    }
}
*/