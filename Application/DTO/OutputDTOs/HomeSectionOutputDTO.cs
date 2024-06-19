namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class HomeSectionOutputDTO
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public List<ProductOutputDTO> Products { get; set; }
    }
}
