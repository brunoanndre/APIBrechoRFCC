namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class ProductOptionInputDTO
    {
        public string? Name { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public int ProductId { get; set; }
    }
}
