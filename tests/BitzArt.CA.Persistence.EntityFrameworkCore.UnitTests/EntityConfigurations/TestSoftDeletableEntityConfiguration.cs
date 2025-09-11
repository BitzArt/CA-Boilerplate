using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.Persistence;

internal class TestSoftDeletableEntityConfiguration : IEntityTypeConfiguration<TestSoftDeletable>
{
    public void Configure(EntityTypeBuilder<TestSoftDeletable> builder)
    {
        builder.ToTable("Deletables.Soft");

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.ConfigureDeletableProperties();

        builder.HasKey(x => x.Id);
    }
}

internal class TestAuditableEntityConfiguration : IEntityTypeConfiguration<TestAuditable>
{
    public void Configure(EntityTypeBuilder<TestAuditable> builder)
    {
        builder.ToTable("Auditables");

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasDefaultValue(string.Empty);
        builder.ConfigureAuditableProperties();

        builder.HasKey(x => x.Id);
    }
}
