namespace APIBrechoRFCC.Core.Entities
{
    public class Category
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public Category(string name, string? image)
        {
            Name = name;
            Image = image;
            IsActive = true;
            CreatedAt = DateTime.Now.ToUniversalTime();   
        }

        public void Update(string name, string? image)
        {
            if(!String.IsNullOrEmpty(name))
            {
                Name = name;
            }
            if(image != null)
            {
                Image = image;
            }
        }


        public void Delete()
        {
            IsActive = false;
        }
    }
}
