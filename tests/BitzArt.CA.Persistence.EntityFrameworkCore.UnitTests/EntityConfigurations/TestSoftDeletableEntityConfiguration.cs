using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.Persistence;

public class TestSoftDeletableEntityConfiguration : IEntityTypeConfiguration<TestSoftDeletable>
{
    public void Configure(EntityTypeBuilder<TestSoftDeletable> builder)
    {
        builder.ToTable("Deletables.Soft");

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.ConfigureDeletableProperties();

        builder.HasKey(x => x.Id);
    }
}
