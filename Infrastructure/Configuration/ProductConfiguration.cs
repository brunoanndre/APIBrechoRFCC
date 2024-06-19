using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.CategoryId).IsRequired();

            //Relação product - category
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            //Relação Product - ProductOption
            builder.HasMany(p => p.Options)
                .WithOne(po => po.Product)
                .HasForeignKey(po => po.ProductId).OnDelete(DeleteBehavior.Cascade);

            //Relação Product - ProductVariant
            builder.HasMany(p => p.Variants)
                .WithOne(pv => pv.Product)
                .HasForeignKey(pv => pv.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
