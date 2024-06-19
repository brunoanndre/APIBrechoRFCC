using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIBrechoRFCC.Infrastructure.Configuration
{
    public class HomeSectionConfiguration : IEntityTypeConfiguration<HomeSection>
    {
        public void Configure(EntityTypeBuilder<HomeSection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("VARCHAR(150)");
        }
    }
}
