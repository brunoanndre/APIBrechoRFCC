namespace APIBrechoRFCC.Core.Entities
{
    public class CartLine
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public  ProductVariant? Variant { get; set; }
        public int CartId { get; set; }
        public  Cart? Cart { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }

        public CartLine(){}
    }
}
