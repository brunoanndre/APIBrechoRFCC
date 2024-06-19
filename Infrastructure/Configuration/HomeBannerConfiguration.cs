using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.Configuration
{
    public class HomeBannerConfiguration : IEntityTypeConfiguration<HomeBanner>
    {
        public void Configure(EntityTypeBuilder<HomeBanner> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("VARCHAR(150)");
            builder.Property(x => x.Position).HasColumnType("int");
            builder.Property(x => x.Image).IsRequired();
        }
    }
}
