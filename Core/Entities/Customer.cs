namespace APIBrechoRFCC.Core.Entities
{
    public class Customer
    {
        public  int Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }

        public Customer(string name, string email,string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }

    }
}
