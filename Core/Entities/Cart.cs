namespace APIBrechoRFCC.Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartLine>? CartLines { get; set; } = new();
        public double SubTotal { get; set; } = 0;
        public double Total { get; set; } = 0;
        public Uri? CheckoutUrl { get; set; } = null;
    }
}
