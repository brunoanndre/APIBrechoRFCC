using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.EntitiesConfiguration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.SubTotal).IsRequired();
            builder.Property(c => c.Total).IsRequired();

            builder.HasMany(c => c.CartLines)
                .WithOne(cl => cl.Cart)
                .HasForeignKey(cl => cl.CartId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
