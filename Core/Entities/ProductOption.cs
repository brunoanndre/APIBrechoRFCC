namespace APIBrechoRFCC.Core.Entities
{
    public class ProductOption
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public int ProductId { get; set; }
        public Product? Product { get; set; }


        public void Update(string? name, List<string>? values, int productId)
        {
            if(!string.IsNullOrEmpty(Name))
            {
                Name = name.Trim();
            }
            if(values != null && values.Count > 0)
            {
                Values = values;
            }
            if(productId > 0)
            {
                ProductId = productId;
            }
        }
    }
}
