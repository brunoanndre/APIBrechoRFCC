using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.EntitiesConfiguration
{
    public class CartLineConfiguration : IEntityTypeConfiguration<CartLine>
    {
        public void Configure(EntityTypeBuilder<CartLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.VariantId).IsRequired().HasColumnType("int");
            builder.Property(x => x.CartId).IsRequired().HasColumnType("int");
            builder.Property(x => x.Quantity).IsRequired().HasColumnType("int");
            builder.Property(x => x.Total).IsRequired().HasColumnType("money");


            builder.HasOne(x => x.Variant)
                .WithMany()
                .HasForeignKey(x => x.VariantId);

            builder.HasOne(x => x.Cart)
                .WithMany(c => c.CartLines)
                .HasForeignKey(x => x.CartId);
                
        }
    }
}
