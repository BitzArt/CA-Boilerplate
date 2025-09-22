using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.Persistence;

public class TestHardDeletableEntityConfiguration : IEntityTypeConfiguration<TestHardDeletable>
{
    public void Configure(EntityTypeBuilder<TestHardDeletable> builder)
    {
        builder.ToTable("Deletables.Hard");

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.ConfigureDeletableProperties();

        builder.HasKey(x => x.Id);
    }
}
