namespace APIBrechoRFCC.Core.Entities
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? Title { get; set; }
        public double OriginalPrice { get; set; }
        public double SellingPrice { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.ToUniversalTime();

    }
}
