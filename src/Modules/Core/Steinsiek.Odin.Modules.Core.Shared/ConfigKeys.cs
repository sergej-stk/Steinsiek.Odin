namespace Steinsiek.Odin.Modules.Core.Shared;

/// <summary>
/// Defines configuration key names used throughout the application.
/// </summary>
public static class ConfigKeys
{
    /// <summary>
    /// Configuration keys for JWT authentication settings.
    /// </summary>
    public static class Jwt
    {
        /// <summary>
        /// JWT signing key configuration path.
        /// </summary>
        public const string Key = "Jwt:Key";

        /// <summary>
        /// JWT token issuer configuration path.
        /// </summary>
        public const string Issuer = "Jwt:Issuer";

        /// <summary>
        /// JWT token audience configuration path.
        /// </summary>
        public const string Audience = "Jwt:Audience";

        /// <summary>
        /// JWT token expiration hours configuration path.
        /// </summary>
        public const string ExpirationHours = "Jwt:ExpirationHours";

        /// <summary>
        /// Default JWT token expiration in hours when not configured.
        /// </summary>
        public const int DefaultExpirationHours = 24;

        /// <summary>
        /// Default JWT issuer value when not configured.
        /// </summary>
        public const string DefaultIssuer = AppInfo.ProductName;

        /// <summary>
        /// Default JWT audience value when not configured.
        /// </summary>
        public const string DefaultAudience = AppInfo.ProductName + ".API";
    }

    /// <summary>
    /// Configuration key for selecting the database provider.
    /// </summary>
    public const string DatabaseProvider = "DatabaseProvider";

    /// <summary>
    /// Database provider name constants.
    /// </summary>
    public static class DatabaseProviders
    {
        /// <summary>
        /// PostgreSQL database provider via Npgsql.
        /// </summary>
        public const string PostgreSql = "PostgreSQL";

        /// <summary>
        /// EF Core InMemory database provider for development and testing.
        /// </summary>
        public const string InMemory = "InMemory";
    }

    /// <summary>
    /// Connection string name constants.
    /// </summary>
    public static class ConnectionStrings
    {
        /// <summary>
        /// PostgreSQL database connection string name.
        /// </summary>
        public const string OdinDb = "odindb";

        /// <summary>
        /// InMemory database name.
        /// </summary>
        public const string InMemoryDbName = "OdinDb";
    }
}
