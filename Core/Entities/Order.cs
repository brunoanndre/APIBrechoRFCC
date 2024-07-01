namespace APIBrechoRFCC.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public double Total { get; set; }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in Items)
            {
                total += item.Total;
            }
            return total;
        }

    }
}
