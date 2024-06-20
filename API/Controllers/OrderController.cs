/*using AutoMapper;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BrechoRFCC.API.Controllers
{
    [Route("v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly OrderRepository _repository;

        public OrderController(IMapper mapper, OrderRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }


        //POST - v1/orders
        [HttpPost]
        public async Task<ActionResult<OrderOutputDTO>> Create([FromBody] OrderOutputDTO input)
        {
            if (input == null) { return BadRequest("Dados inválidos"); }

            var order = _mapper.Map<Order>(input);

            var orderDB = await _repository.Create(order);

            var orderOutputDTO = _mapper.Map<OrderOutputDTO>(orderDB);
            return CreatedAtAction("GetById", new { Id = orderOutputDTO.Id }, orderOutputDTO);
        }

        //GET - v1/orders
        [HttpGet]
        public async Task<ActionResult<List<OrderOutputDTO>>> GetAll()
        {
            List<Order> orders = await _repository.GetAll();
            var orderDTO = _mapper.Map<List<OrderOutputDTO>>(orders);

            return Ok(orderDTO);
        }

        //GET - v1/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderOutputDTO>> GetById(Guid id)
        {
            try
            {
                Order order = await _repository.GetById(id);
                var orderDTO = _mapper.Map<OrderOutputDTO>(order);
                return Ok(orderDTO);
            } catch (OrderNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        //PUT - v1/orders/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderOutputDTO>> Update(Guid id, [FromBody] OrderInputDTO input)
        {
            if (!String.IsNullOrEmpty(id.ToString()) || input == null) return BadRequest("Dados inválidos.");
            try
            {
                Order order = _mapper.Map<Order>(input);
                order.Id = id;
                Order updatedOrder = await _repository.Update(order);

                OrderOutputDTO orderDTO = _mapper.Map<OrderOutputDTO>(updatedOrder);
                return Ok(orderDTO);

            }catch(OrderNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        //DELETE - v1/orders/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                if(await _repository.Delete(id)) return Ok();


                return StatusCode(500, "Um erro inesperado ocorreu, tente novamente.");
            }
            catch(OrderNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
*/