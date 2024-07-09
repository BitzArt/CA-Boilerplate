using BitzArt.CA.SampleApp.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.SampleApp.Persistence;

internal class BookAggregateConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Author)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
