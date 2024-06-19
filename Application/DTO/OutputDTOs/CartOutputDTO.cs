namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class CartOutputDTO
    {
        public string? Id { get; set; } = null;
        public List<CartLineOutputDTO>? Lines { get; set; } = [];
        public double? Subtotal { get; set; } = null;
        public double? Total { get; set; } = null;
        public Uri? CheckoutUrl { get; set; } = null;
    }
}
