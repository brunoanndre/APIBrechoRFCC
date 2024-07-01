using AutoMapper;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using APIBrechoRFCC.Infrastructure.Interface;

namespace BrechoRFCC.API.Controllers
{
    [Route("v1/checkout")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly OrderRepository _repository;
        private readonly ICRUDRepository<ProductVariant> _productVariantRepository;

        public OrderController(IMapper mapper, OrderRepository repository,ICRUDRepository<ProductVariant> variantRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _productVariantRepository = variantRepository;
        }


        //POST - v1/orders
        [HttpPost("confirm-reservation")]
        public async Task<ActionResult<OrderOutputDTO>> Create([FromBody] OrderInputDTO input)
        {
            if (input == null) { return BadRequest("Dados inválidos"); }
            try
            {
                List<OrderItem> items = [];
                foreach (int item in input.ProductVariantIds)
                {
                    var productVariant = await _productVariantRepository.GetById(item);
                    OrderItem orderItem = new()
                    {
                        ProductVariantId = productVariant.Id,
                        Title = productVariant.Title,
                        Quantity = 1,
                        Total = productVariant.SellingPrice
                    };
                    items.Add(orderItem);
                }

                var order = _mapper.Map<Order>(input);
                Guid guid = Guid.NewGuid();
                order.Id = guid;
                order.Items = items;
                order.Total = order.CalculateTotal();

                await _repository.Create(order);

                var orderOutputDTO = _mapper.Map<OrderOutputDTO>(order);
                return Ok(orderOutputDTO);
            }
            catch(ProductVariantNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        //GET - v1/orders
        [HttpGet]
        public async Task<ActionResult<List<OrderOutputDTO>>> GetAll()
        {
            List<Order> orders = await _repository.GetAll();

            if (orders == null || orders.Count <= 0) return NotFound("Nenhuma reserva encontrada.");
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
    }
}
