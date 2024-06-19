namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class CartLineOutputDTO
    {
        public string? Id { get; set; } = null;
        public ProductVariantOutputDTO? ProductVariant { get; set; } = null;
        public int? Quantity { get; set; } = null;
        public double? Total { get; set; } = null;
    }
}
