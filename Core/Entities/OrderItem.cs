﻿namespace APIBrechoRFCC.Core.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}
