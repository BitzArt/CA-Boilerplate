using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.Persistence;

public class TestForwardableEntityConfiguration : IEntityTypeConfiguration<TestForwardable>
{
    public void Configure(EntityTypeBuilder<TestForwardable> builder)
    {
        builder.ToTable("Forwardables");

        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasDefaultValue(string.Empty);

        // Standalone shadow property
        builder.Property<string>("ShadowName").IsRequired().HasDefaultValue(string.Empty);

        // Shadow property mapped to CLR property
        builder.Property<string>($"{nameof(TestForwardable.Name)}").IsRequired().HasDefaultValue(string.Empty);
        

        builder.HasKey(x => x.Id);
    }
}
