using APIBrechoRFCC.Core.Entities.Enums;
namespace APIBrechoRFCC.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int OrderNumber { get; set; }
        public required List<OrderItem> Items { get; set; }
        public OrderStatus Status{ get; set; }
        public double Total { get; set; }

    }
}
