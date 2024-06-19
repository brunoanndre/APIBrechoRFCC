using AutoMapper;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using APIBrechoRFCC.Infrastructure.Services;

namespace APIBrechoRFCC.API.Controllers
{
    [Route("v1/productVariant")]
    [ApiController]
    public class ProductVariantController : ControllerBase
    {
        private readonly ICRUDRepository<ProductVariant> _repository;
        private readonly IMapper _mapper;
        private readonly FirebaseStorageService _firebaseStorageService;
        public ProductVariantController(ICRUDRepository<ProductVariant> repository, IMapper mapper,FirebaseStorageService storageService)
        {
            _repository = repository;
            _mapper = mapper;
            _firebaseStorageService = storageService;
        }

        //POST - v1/productVariant
        [HttpPost]
        public async Task<ActionResult<ProductVariantOutputDTO>> Create([FromForm] ProductVariantInputDTO model)
        {
            try
            {
                if (model == null) { return BadRequest("Dados inválidos."); }

                var product = _mapper.Map<ProductVariant>(model);
                if (model.Image != null)
                {
                    var imageUrl = await _firebaseStorageService.UploadFileAsync(model.Image);
                    product.Image = imageUrl;
                }

                var productDB = await _repository.Create(product);

                var productDTO = _mapper.Map<ProductVariantOutputDTO>(productDB);
                return CreatedAtAction("GetById", new { id = productDTO.Id }, productDTO);
            }catch(ProductNotFoundException e)
            {
                return BadRequest(e.Message);
            }

        }

        //GET - v1/productVariant
        [HttpGet]
        public async Task<ActionResult<List<ProductVariantOutputDTO>>> GetAll()
        {
            var productVariants = await _repository.GetAll();
            if(productVariants.Count == 0) { return NotFound("Nenhuma variante cadastrada."); }
            var productVariantsDTO = _mapper.Map<List<ProductVariantOutputDTO>>(productVariants);
            return Ok(productVariantsDTO);
        }

        //GET - v1/productVariant/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVariantOutputDTO>> GetById(int id)
        {
            if (id == 0) { return BadRequest("Id inválido."); }
            try
            {
                var productVariantDB = await _repository.GetById(id);
                var productVariantDTO = _mapper.Map<ProductVariantOutputDTO>(productVariantDB);
                return Ok(productVariantDTO);
            }
            catch(ProductVariantNotFoundException e)
            {
                return NotFound(e.Message);
            }       
        }

        //PUT - v1/productVariant/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductVariantOutputDTO>> Update(int id, [FromForm] ProductVariantInputDTO input)
        {
            if (id == 0) { return BadRequest("Id inválido."); }

            try
            {
                var productVariant = await _repository.GetById(id);
                productVariant.Id = id;
                if(input.Image != null)
                {
                    if (!string.IsNullOrEmpty(productVariant.Image))
                    {
                        var oldImage = new Uri(productVariant.Image).AbsolutePath.Split("/").Last(); // Remove o "/" inicial
                        await _firebaseStorageService.DeleteFileAsync(oldImage);
                    }

                    //Upload da nova imagem no firebase
                    productVariant.Image = await _firebaseStorageService.UploadFileAsync(input.Image);
                }

                var variantDb = await _repository.Update(productVariant);

                var productVariantDTO = _mapper.Map<ProductVariantOutputDTO>(variantDb);
                return Ok(productVariantDTO);
            }
            catch (ProductVariantNotFoundException e)
            {
                return NotFound(e.Message);
            }


        }

        //DELETE - v1/productVariant/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok();
            }catch(ProductVariantNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
