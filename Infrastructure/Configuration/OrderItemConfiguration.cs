using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductVariantId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Total).IsRequired();    

        }
    }
}
