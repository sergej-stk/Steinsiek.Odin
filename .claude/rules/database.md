# Database

## Database Configuration

### Provider Switch
The application supports two database providers, configured via `DatabaseProvider` in `appsettings.json`:

| Provider | Value | Use Case |
|----------|-------|----------|
| PostgreSQL | `"PostgreSQL"` | Production, Aspire orchestration |
| EF InMemory | `"InMemory"` | Development without DB, integration tests |

When running via Aspire AppHost, `DatabaseProvider` is set to `"PostgreSQL"` via environment variable.

### Connection Strings
- PostgreSQL connection string is provided by Aspire via `ConnectionStrings:odindb`
- InMemory mode requires no connection string

### Database Schemas
Each module owns a dedicated schema:

| Module | Schema | Tables |
|--------|--------|--------|
| Core | `core` | `Languages`, `Translations`, `Salutations`, `Genders`, `MaritalStatuses`, `Countries`, `AddressTypes`, `ContactTypes`, `Industries`, `LegalForms`, `Departments`, `Positions`, `AuditLog` |
| Auth | `auth` | `Users`, `Roles`, `UserRoles` |
| Persons | `persons` | `Persons`, `PersonAddresses`, `PersonEmailAddresses`, `PersonPhoneNumbers`, `PersonBankAccounts`, `PersonSocialMediaLinks`, `PersonLanguages`, `PersonImages` |
| Companies | `companies` | `Companies`, `CompanyLocations`, `CompanyImages`, `LocationImages`, `PersonCompanies` |

### Auto-Migration
- **PostgreSQL**: `Database.Migrate()` runs at startup, applying pending migrations
- **InMemory**: `Database.EnsureCreated()` creates the schema from the model

## Entity Framework Core

### OdinDbContext
- Central `DbContext` in `Steinsiek.Odin.Modules.Core.Persistence`
- No direct `DbSet<T>` properties - modules use `context.Set<T>()`
- Entity configurations are discovered via `ApplyConfigurationsFromAssembly()` from registered module assemblies
- Module assemblies registered via `OdinDbContextOptions`

### Entity Type Configurations
Each entity has an `IEntityTypeConfiguration<T>` in its module's `Persistence/Configurations/` folder:
- Table name and schema assignment
- Property constraints (MaxLength, Precision, Required)
- Relationships and foreign keys
- Unique indexes
- Seed data via `HasData()`

### Migrations
- Stored in `src/Modules/Core/Steinsiek.Odin.Modules.Core/Migrations/`
- Generated via `dotnet ef` CLI with the API as startup project
- `DesignTimeDbContextFactory` in the API project provides migration tooling support

### Migration Commands
```bash
# Add a new migration
dotnet ef migrations add {MigrationName} --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API --output-dir Migrations

# Remove last migration (if not applied)
dotnet ef migrations remove --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API

# List migrations
dotnet ef migrations list --project src/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API
```

### Repository Pattern with EF Core
- Repositories use `OdinDbContext` injected via primary constructor
- Access entities via `context.Set<T>()` (no typed DbSet properties)
- All repositories are **Scoped** (not Singleton)
- `SaveChangesAsync()` called within each repository method

## Password Hashing

### IPasswordHasher Interface
- `Hash(string password)` - creates a BCrypt hash
- `Verify(string password, string hash)` - verifies password against hash

### Implementation
- `BcryptPasswordHasher` uses BCrypt.Net-Next (registered as Singleton)
- Password hashing happens in the `AuthService`, not in the repository
- Seed data uses a pre-computed BCrypt hash constant for migration stability

### Migration from SHA256
The project previously used SHA256 hashing. All password hashes are now BCrypt format (`$2a$11$...`).
