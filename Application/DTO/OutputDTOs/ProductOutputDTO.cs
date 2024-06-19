namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class ProductOutputDTO
    {
        public string? Id { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null;
        public List<string>? Images { get; set; } = null;
        public List<ProductVariantOutputDTO>? Variants { get; set; } = null;
        public List<ProductOptionOutputDTO>? Options { get; set; } = null;
    }
}
