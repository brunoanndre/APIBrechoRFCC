using AutoMapper;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Core.Exceptions;
using APIBrechoRFCC.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using APIBrechoRFCC.Infrastructure.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIBrechoRFCC.API.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICRUDRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly FirebaseStorageService _firebaseStorageService;

        public ProductController(ICRUDRepository<Product> repository, IMapper mapper,IWebHostEnvironment environment, FirebaseStorageService firebaseStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _firebaseStorageService = firebaseStorageService;
        }
        // GET: v1/products
        [HttpGet]
        public async Task<ActionResult<List<ProductOutputDTO>>> GetAll()
        {
            var products = await _repository.GetAll();

            if (products is null || products.Count <= 0) return NotFound("Não há produtos cadastrados.");

            var productsDTO = _mapper.Map<List<ProductOutputDTO>>(products);
            return Ok(productsDTO);
        }

        // GET v1/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOutputDTO>> GetById(int id)
        {
            if (id == 0) return BadRequest("Id do produto inválido.");
            try
            {
                var product = await _repository.GetById(id);

                var productDto = _mapper.Map<ProductOutputDTO>(product);

                return Ok(productDto);
            }catch(ProductNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // POST v1/products
        [HttpPost]
        public async Task<ActionResult<ProductOutputDTO>> Post([FromForm] ProductInputDTO model)
        {
            try
            {
                if (model == null) return BadRequest("Dados do produto inválidos");
                var product = _mapper.Map<Product>(model);

                if(model.Images != null && model.Images.Count > 0)
                {
                    var imageUrls = await _firebaseStorageService.UploadFileListAsync(model.Images);
                    product.Images = imageUrls;
                }
                
                var productDb = await _repository.Create(product);

                var productDTO = _mapper.Map<ProductOutputDTO>(productDb);
                return CreatedAtAction("GetById", new { id = productDTO.Id }, productDTO);
            }
            catch(CategoryNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT  -v1/products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductOutputDTO>> Put(int id, [FromForm] ProductInputDTO model)
        {
            var product = await _repository.GetById(id);
            if (product == null) return NotFound("Produto não encontrado.");

            if (model.Images != null && model.Images.Count > 0)
            {
                //Remover imagens atuais p/ adicionar novas.   -- ALTERAR ISSO AQUI P/ ADICIONAR SOMENTE AS IMAGENS NOVAS E REMOVER SOMENTE AS IMAGENS QUE NÃO ESTÃO MAIS NO MODEL
                if(product.Images != null && product.Images.Count > 0)
                {    
                    foreach(var image in product.Images)
                    {
                        var oldImage = new Uri(image).AbsolutePath.Split("/").Last(); // Remove o "/" inicial
                        await _firebaseStorageService.DeleteFileAsync(oldImage);
                    }
                }

                //Adicionar nova imagens
                var imageUrls = await _firebaseStorageService.UploadFileListAsync(model.Images);
                product.Images = imageUrls;
            }
            else
            {
                //Remover imagens
                if (product.Images != null && product.Images.Count > 0)
                {
                    foreach (var image in product.Images)
                    {
                        await _firebaseStorageService.DeleteFileAsync(image);
                    }
                }
            }
            
            product.Title = model.Title;
            product.Description = model.Description;
            product.CategoryId = model.CategoryId;

            var productUpdated =  await _repository.Update(product);

            var productOutputDTO = _mapper.Map<ProductOutputDTO>(productUpdated);
            return Ok(productOutputDTO);
        }
    }
}
