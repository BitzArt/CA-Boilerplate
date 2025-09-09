using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Extension methods for configuring <see cref="ISoftDeletable"/> entity properties.
/// </summary>
public static class DeletableConfigurationExtensions
{
    /// <summary>
    /// Configures <see cref="ISoftDeletable{TDeletionInfo}"/> entity properties.
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{T}"/> to use for property configuration.</param>
    public static void ConfigureDeletableProperties<TEntity, TDeletionInfo>(this EntityTypeBuilder<TEntity> builder, Action<OwnedNavigationBuilder<TEntity, TDeletionInfo>> configureDeletionInfo)
        where TEntity : class, ISoftDeletable<TDeletionInfo>
        where TDeletionInfo : class
    {
        ConfigureIsDeletedProperty(builder);
        TryConfigureIsHardDeletedProperty(builder);

        builder.OwnsOne(x => x.DeletionInfo, configureDeletionInfo);
    }

    /// <summary>
    /// Configures <see cref="ISoftDeletable"/> entity properties.
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{T}"/> to use for property configuration.</param>
    public static void ConfigureDeletableProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ISoftDeletable
    {
        ConfigureIsDeletedProperty(builder);
        TryConfigureIsHardDeletedProperty(builder);
        TryConfigureDefaultDeletionInfo(builder);
    }

    private static void ConfigureIsDeletedProperty<TEntity>(EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ISoftDeletable
    {
        builder.Property(nameof(ISoftDeletable.IsDeleted)).IsRequired(true);

        builder.HasIndex(nameof(ISoftDeletable.IsDeleted));

        builder.HasQueryFilter(e => e.IsDeleted != true);
    }

    private static void TryConfigureIsHardDeletedProperty<TEntity>(EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ISoftDeletable
    {
        if (!typeof(IHardDeletable).IsAssignableFrom(typeof(TEntity)))
        {
            // The entity does not implement IHardDeletable
            return;
        }

        builder.Ignore(nameof(IHardDeletable.IsHardDeleted));
    }

    private static void TryConfigureDefaultDeletionInfo<TEntity>(EntityTypeBuilder<TEntity> builder)
        where TEntity : class, ISoftDeletable
    {
        var builderType = builder.GetType();

        // Recursively navigate up the inheritance chain to EntityTypeBuilder<TEntity>
        // (for cases where the builder somehow turned out to be a derived type,
        // not sure if it can actually happen in practice - but better safe than sorry)
        while (builderType.IsGenericType == false || builderType.GetGenericTypeDefinition() != typeof(EntityTypeBuilder<>))
        {
            if (builderType.BaseType == null)
            {
                throw new UnreachableException("Unable to locate generic base type EntityTypeBuilder<>");
            }

            builderType = builderType.BaseType;
        }

        var entityType = builderType.GetGenericArguments()[0];

        var softDeletableInterface = entityType.GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ISoftDeletable<>));

        if (softDeletableInterface == null)
        {
            // The entity does not implement ISoftDeletable<T>
            return;
        }

        var deletionInfoType = entityType.GetGenericArguments()[0];

        // NOTE: We do not configure descendants of DeletionInfo here by design
        if (deletionInfoType != typeof(DeletionInfo))
        {
            // The deletion info type is not the default DeletionInfo type
            return;
        }

        builder.OwnsOne<DeletionInfo>(nameof(ISoftDeletable<DeletionInfo>.DeletionInfo), deletionInfo =>
        {
            deletionInfo.Property(x => x.DeletedAt).HasColumnName(nameof(DeletionInfo.DeletedAt)).IsRequired(false);

            deletionInfo.HasIndex(x => x.DeletedAt)
                .HasFilter($"[{nameof(DeletionInfo.DeletedAt)}] IS NOT NULL");
        });
    }
}
