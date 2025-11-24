using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.Persistence;

public class WarmupEntityConfiguration : IEntityTypeConfiguration<WarmupEntity>
{
    public void Configure(EntityTypeBuilder<WarmupEntity> builder)
    {
        builder.ToTable("WarmupEntities");
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasKey(x => x.Id);
    }
}
