using Microsoft.AspNetCore.Mvc;
using APIBrechoRFCC.Core.Entities;
using AutoMapper;
using APIBrechoRFCC.Infrastructure.Interface;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Application.DTO.OutputDTOs;
using static System.Net.Mime.MediaTypeNames;
using APIBrechoRFCC.Core.Exceptions;
using FirebaseAdmin;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;
using APIBrechoRFCC.Infrastructure.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIBrechoRFCC.API.Controllers
{
    [Route("v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICRUDRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private readonly StorageClient _storageClient;
        private readonly FirebaseStorageService _firebaseStorageService;


        public CategoryController(IWebHostEnvironment environment, ICRUDRepository<Category> repository, IMapper mapper, FirebaseStorageService firebaseStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
            _firebaseStorageService = firebaseStorageService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<List<CategoryOutputDTO>>> GetAll()
        {
            var categories = await _repository.GetAll();

            if (categories is null || categories.Count <= 0) return NotFound("Não há categorias cadastradas.");

            var viewModel = _mapper.Map<List<CategoryOutputDTO>>(categories);
            return Ok(viewModel);
        }

        // GET v1/categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryOutputDTO>> GetById(int id)
        {
            if(id == 0) { return BadRequest(); }
            try
            {
                var category = await _repository.GetById(id);

                var viewModel = _mapper.Map<CategoryOutputDTO>(category);
                return Ok(viewModel);
            }
            catch(CategoryNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
        //GET v1/categories/{id}/products
        [HttpGet("{id}/products")]
        public async Task<ActionResult<List<ProductOutputDTO>>> GetProductsByCategoryId(int id)
        {
            var category = await _repository.GetById(id);
            if (category == null) return NotFound("Categoria não encontrada.");

            
            var products = _mapper.Map<List<ProductOutputDTO>>(category.Products);
            if (products.Count <= 0) return NotFound($"Nenhum produto cadastrado.");
            return Ok(products);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult<CategoryOutputDTO>> Post([FromForm] CategoryInputDTO model)
        {
            var category = _mapper.Map<Category>(model);
            
            if(model.Image != null)
            {  
               var imageUrl = await _firebaseStorageService.UploadFileAsync(model.Image);
               category.Image = imageUrl;
            }
            var categoryDb = await _repository.Create(category);
            
            var viewModel = _mapper.Map<CategoryOutputDTO>(categoryDb);
            return CreatedAtAction("GetById", new {id = viewModel.Id}, viewModel);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] CategoryInputDTO model)
        {
            try
            {
                var category = await _repository.GetById(id);
                category.Id = id;
                category.Name = model.Name;
                //Se houver imagem para atualizar, deletar a imagem atual do firebase e adicionar a nova
                if (model.Image != null)
                {
                    if (!string.IsNullOrEmpty(category.Image.ToString()))
                    {
                        var oldImage = new Uri(category.Image).AbsolutePath.Split("/").Last(); // Remove o "/" inicial
                        await _firebaseStorageService.DeleteFileAsync(oldImage);
                    }

                    //Upload da nova imagem no firebase
                    
                    category.Image = await _firebaseStorageService.UploadFileAsync(model.Image);
                }
                
                
                var categoryDB = await _repository.Update(category);

                var categoryOutputDTO = _mapper.Map<CategoryOutputDTO>(categoryDB);

                return Ok(categoryOutputDTO);

            }
            catch(CategoryNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok();
            }
            catch(CategoryNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
