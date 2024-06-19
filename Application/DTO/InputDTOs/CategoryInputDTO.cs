namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class CategoryInputDTO
    {
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
