namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class OrderItemOutputDTO
    {
        public string? Title { get; set; }
        public ProductVariantOutputDTO? ProductVariant { get; set; }
        public double Total { get; set; }
    }
}
