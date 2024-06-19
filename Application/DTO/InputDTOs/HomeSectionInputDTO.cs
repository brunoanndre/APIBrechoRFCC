using APIBrechoRFCC.Application.DTO.OutputDTOs;

namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class HomeSectionInputDTO
    {
        public string? Title { get; set; }
        public List<ProductOutputDTO> Products { get; set; }
    }
}
