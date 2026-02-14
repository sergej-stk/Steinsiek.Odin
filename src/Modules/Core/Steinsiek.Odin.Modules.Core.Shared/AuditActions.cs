namespace Steinsiek.Odin.Modules.Core.Shared;

/// <summary>
/// Defines standard audit log action types recorded by the audit interceptor.
/// </summary>
public static class AuditActions
{
    /// <summary>
    /// Action recorded when a new entity is created.
    /// </summary>
    public const string Created = "Created";

    /// <summary>
    /// Action recorded when an existing entity property is modified.
    /// </summary>
    public const string Updated = "Updated";

    /// <summary>
    /// Action recorded when an entity is deleted (soft-delete).
    /// </summary>
    public const string Deleted = "Deleted";
}
