namespace Steinsiek.Odin.Modules.Core.Shared;

/// <summary>
/// Defines PostgreSQL schema names for database table organization per module.
/// </summary>
public static class OdinSchemas
{
    /// <summary>
    /// Authentication schema containing Users, Roles and UserRoles tables.
    /// </summary>
    public const string Auth = "auth";

    /// <summary>
    /// Core schema containing lookup tables, translations and audit log.
    /// </summary>
    public const string Core = "core";

    /// <summary>
    /// Persons schema containing person-related tables.
    /// </summary>
    public const string Persons = "persons";

    /// <summary>
    /// Companies schema containing company-related tables.
    /// </summary>
    public const string Companies = "companies";
}
