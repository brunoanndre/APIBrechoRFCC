using AutoMapper;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;


namespace APIBrechoRFCC.API.Controllers
{
    [Route("v1/productOption")]
    [ApiController]
    public class ProductOptionController : ControllerBase
    {
        private readonly ICRUDRepository<ProductOption> _repository;
        private readonly IMapper _mapper;

        public ProductOptionController(ICRUDRepository<ProductOption> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductOptionOutputDTO>>> GetAll()
        {
            var productOptions = await _repository.GetAll();

            if (productOptions is null || productOptions.Count <= 0) return NotFound("Nenhuma opção de produto cadastrada.");

            var productOptionsDTO = _mapper.Map<List<ProductOptionOutputDTO>>(productOptions);

            return Ok(productOptionsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOptionOutputDTO>> GetById(int id)
        {
            if (id <= 0) return BadRequest("Id inválido");

            var productOption = await _repository.GetById(id);

            if (productOption is null) return NotFound("");

            var productOptionDTO = _mapper.Map<ProductOptionOutputDTO>(productOption);

            return Ok(productOptionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductOptionOutputDTO>> Post([FromBody] ProductOptionInputDTO model)
        {
            try
            {
                var productOption = _mapper.Map<ProductOption>(model);

                var productOptionDb = await _repository.Create(productOption);

                var productOptionDTO = _mapper.Map<ProductOptionOutputDTO>(productOptionDb);
                return CreatedAtAction("GetById", new { id = productOptionDTO.Id }, productOptionDTO);
            }catch(ProductNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductOptionOutputDTO>> Put([FromBody] ProductOptionInputDTO model, int id)
        {
            try
            {
                var productOption = await _repository.GetById(id);

                productOption.Update(model.Name, model.Values, model.ProductId);

                var productOptionDB = await _repository.Update(productOption);

                var productOptionDTO = _mapper.Map<ProductOptionOutputDTO>(productOptionDB);
                return Ok(productOptionDTO);
            }
            catch (ProductOptionNotFoundException e)
            {
                return BadRequest(e.Message);
            }catch(ProductNotFoundException e)
            {
                return BadRequest(e.Message);
            }

        }

        //DELETE - v1/productOption/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok();
            }catch(ProductOptionNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
