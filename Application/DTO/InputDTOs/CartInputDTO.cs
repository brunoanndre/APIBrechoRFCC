using APIBrechoRFCC.Application.DTO.OutputDTOs;
namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class CartInputDTO
    {
        public List<CartLineOutputDTO>? Lines { get; set; } = null;
        public double? Subtotal { get; set; } = null;
        public double? Total { get; set; } = null;
        public Uri? CheckoutUrl { get; set; } = null;
    }
}
