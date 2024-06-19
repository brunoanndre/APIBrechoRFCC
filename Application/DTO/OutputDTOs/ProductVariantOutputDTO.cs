namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class ProductVariantOutputDTO
    {
        public string? Id { get; set; } = null;
        public string? ProductId { get; set; } = null;
        public double? OriginalPrice { get; set; } = null;
        public double? SellingPrice { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Image { get; set; } = null;
    }
}
