using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.Configuration
{
    public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.HasKey(po => po.Id);

            builder.Property(po => po.Name).IsRequired();
            builder.Property(po => po.Values).IsRequired();

            builder.HasOne(po => po.Product)
                .WithMany(p => p.Options)
                .HasForeignKey(po => po.ProductId);
        }
    }
}
