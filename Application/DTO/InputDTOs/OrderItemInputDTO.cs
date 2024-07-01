using APIBrechoRFCC.Core.Entities;

namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class OrderItemInputDTO
    {
        public string? Title { get; set; }
        public Guid OrderId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
