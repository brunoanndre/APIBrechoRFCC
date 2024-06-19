using APIBrechoRFCC.Application.DTO.OutputDTOs;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Infrastructure.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBrechoRFCC.API.Controllers
{
    [Route("v1/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly HomeRepository _repository;
        private readonly IMapper _mapper;

        public HomeController(HomeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<HomeBannerOutputDTO>>> GetBanners()
        {
            var banners = await _repository.GetBanners();

            if (banners == null || banners.Count == 0) return NotFound("Nenhum banner encontrado.");

            var bannersDTO = _mapper.Map<List<HomeBannerOutputDTO>>(banners);
            return Ok(bannersDTO);
        }

        [HttpGet("/sections")]
        public async Task<ActionResult<List<HomeSectionOutputDTO>>> GetSections()
        {
            var sections = await _repository.GetSections();
            if (sections == null || sections.Count == 0) return NotFound("Nenhuma seção encontrada.");

            var sectionsDTO = _mapper.Map<List<HomeSectionOutputDTO>>(sections);
            return Ok(sectionsDTO);
        }

        [HttpPost]
        public async Task<ActionResult<HomeBannerOutputDTO>> CreateBanner(HomeBannerOutputDTO bannerDTO)
        {
            if(bannerDTO == null) throw new ArgumentNullException(nameof(bannerDTO));

            var banner = _mapper.Map<HomeBanner>(bannerDTO);

            await _repository.CreateHomeBanner(banner);

            return Ok(banner);
        }

        [HttpPost("/sections")]
        public async Task<ActionResult<List<HomeSectionOutputDTO>>> CreateSection(HomeSectionOutputDTO sectionDTO)
        {
            if (sectionDTO == null) throw new ArgumentNullException(nameof(sectionDTO));

            var section = _mapper.Map<HomeSection>(sectionDTO);
            await _repository.CreateHomeSection(section);
            return Ok(section);
        }


    }
}
