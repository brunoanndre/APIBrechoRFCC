using APIBrechoRFCC.Core.Entities;

namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class OrderInputDTO
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public List<int> ProductVariantIds { get; set; }
        
    }
}
