namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class ProductInputDTO
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Images { get; set; }
        public required int CategoryId { get; set; }
    }
}
