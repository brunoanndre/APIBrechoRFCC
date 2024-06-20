namespace APIBrechoRFCC.Core.Entities
{
    public class Customer
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

    }
}
