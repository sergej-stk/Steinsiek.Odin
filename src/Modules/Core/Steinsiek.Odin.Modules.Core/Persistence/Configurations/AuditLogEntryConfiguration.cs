namespace Steinsiek.Odin.Modules.Core.Persistence.Configurations;

/// <summary>
/// Entity type configuration for <see cref="AuditLogEntry"/>.
/// </summary>
public sealed class AuditLogEntryConfiguration : IEntityTypeConfiguration<AuditLogEntry>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuditLogEntry> builder)
    {
        builder.ToTable("AuditLog", "core");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.EntityType)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.Action)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.PropertyName)
            .HasMaxLength(256);

        builder.Property(e => e.OldValue)
            .HasMaxLength(4000);

        builder.Property(e => e.NewValue)
            .HasMaxLength(4000);

        builder.HasIndex(e => new { e.EntityType, e.EntityId });
        builder.HasIndex(e => new { e.UserId, e.Timestamp });
    }
}
