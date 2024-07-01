using APIBrechoRFCC.Core.Entities.Enums;

namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class OrderOutputDTO
    {
        public string Id { get; set; } = String.Empty;
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public List<OrderItemOutputDTO>? Items { get; set; } = null;
        public double Total { get; set; }
    }
}
