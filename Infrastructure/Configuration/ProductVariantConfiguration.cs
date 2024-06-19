using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.Configuration
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.HasKey(pv => pv.Id);
            builder.Property(pv => pv.Title).IsRequired().HasMaxLength(100);
            builder.Property(pv => pv.ProductId).IsRequired();
            builder.Property(pv => pv.OriginalPrice).IsRequired();
            builder.Property(pv => pv.SellingPrice).IsRequired();

            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(pv => pv.ProductId);

        }
    }
}
