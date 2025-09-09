using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Extension methods for configuring <see cref="ICreatedAt"/> and <see cref="IAuditable"/> entity properties.
/// </summary>
public static class AuditableConfigurationExtensions
{
    /// <summary>
    /// Configures <see cref="ICreatedAt"/> and <see cref="IAuditable"/> entity properties.
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{T}"/> to use for property configuration.</param>
    public static void ConfigureAuditableProperties<T>(this EntityTypeBuilder<T> builder)
        where T : class, ICreatedAt
    {
        builder.Property(nameof(ICreatedAt.CreatedAt)).IsRequired(true);

        if (typeof(IAuditable).IsAssignableFrom(typeof(T)))
        {
            builder.Property(nameof(IAuditable.LastUpdatedAt)).IsRequired(true);
        }
    }
}
