namespace APIBrechoRFCC.Core.Entities
{
    public class Product
    {
        public  int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedAt { get; set; }
        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public List<ProductOption> Options { get; set; } = new List<ProductOption>();

        public void Update(string title,string? description,List<string>? images,int categoryId)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Title = title;
            }
            if(!string.IsNullOrEmpty(description))
            {
                Description = description;
            }
            if(images != null && images.Count > 0)
            {
                Images = images;
            }
            if(categoryId != 0)
            {
                CategoryId = categoryId;
            }
        }
    }
}
