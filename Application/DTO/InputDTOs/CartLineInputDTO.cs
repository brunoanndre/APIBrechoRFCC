namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class CartLineInputDTO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity{ get; set; }
        public double Total { get; set; }
    }
}
