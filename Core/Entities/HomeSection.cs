namespace APIBrechoRFCC.Core.Entities
{
    public class HomeSection
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<Product>? Products { get; set; }
    }
}
