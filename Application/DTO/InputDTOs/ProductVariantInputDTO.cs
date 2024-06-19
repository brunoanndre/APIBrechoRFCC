namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class ProductVariantInputDTO
    {
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public double OriginalPrice { get; set; }
        public double SellingPrice { get; set; }
        public IFormFile? Image { get; set; }
    }
}
