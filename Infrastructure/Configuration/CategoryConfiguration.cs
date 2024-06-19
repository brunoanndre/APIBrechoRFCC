using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace APIBrechoRFCC.Infrastructure.Configuration
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Image).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();

            builder.HasMany(x => x.Products).WithOne(c => c.Category).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
