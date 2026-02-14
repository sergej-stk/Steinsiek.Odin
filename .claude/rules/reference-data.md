# Reference Data

## API Endpoints

### Auth
| Endpoint | Auth | Description |
|----------|------|-------------|
| POST /api/v1/auth/login | No | Login |
| POST /api/v1/auth/register | No | Registration |
| GET /api/v1/auth/me | Yes | Current user |
| GET /api/v1/auth/roles | Yes (Admin) | List all roles |
| POST /api/v1/auth/users/{userId}/roles | Yes (Admin) | Assign role |
| DELETE /api/v1/auth/users/{userId}/roles/{roleId} | Yes (Admin) | Remove role |

### Persons
| Endpoint | Auth | Description |
|----------|------|-------------|
| GET /api/v1/persons | Yes | All persons (paginated) |
| GET /api/v1/persons/{id} | Yes | Person detail |
| GET /api/v1/persons/search?q= | Yes | Search persons |
| POST /api/v1/persons | Yes | Create person |
| PUT /api/v1/persons/{id} | Yes | Update person |
| DELETE /api/v1/persons/{id} | Yes | Soft-delete person |

### Companies
| Endpoint | Auth | Description |
|----------|------|-------------|
| GET /api/v1/companies | Yes | All companies (paginated) |
| GET /api/v1/companies/{id} | Yes | Company detail |
| GET /api/v1/companies/search?q= | Yes | Search companies |
| POST /api/v1/companies | Yes | Create company |
| PUT /api/v1/companies/{id} | Yes | Update company |
| DELETE /api/v1/companies/{id} | Yes | Soft-delete company |

### Dashboard
| Endpoint | Auth | Description |
|----------|------|-------------|
| GET /api/v1/dashboard/stats | Yes | Aggregate statistics |
| GET /api/v1/dashboard/recent-activity | Yes | Recent audit log entries |
| GET /api/v1/dashboard/upcoming-birthdays | Yes | Upcoming birthdays |

### Core
| Endpoint | Auth | Description |
|----------|------|-------------|
| GET /api/v1/lookups | Yes | Translated lookup values |
| GET /api/v1/auditlog/{entityType}/{entityId} | Yes (Admin) | Audit log for entity |
| GET /api/v1/auditlog/recent | Yes (Admin) | Recent audit entries |

## Configuration

### appsettings.json Structure
```json
{
  "Serilog": { ... },
  "Jwt": {
    "Key": "...",
    "Issuer": "Steinsiek.Odin",
    "Audience": "Steinsiek.Odin.API",
    "ExpirationHours": 24
  },
  "DatabaseProvider": "InMemory"
}
```

- `DatabaseProvider`: `"InMemory"` (default) or `"PostgreSQL"` (set by Aspire)
- `ConnectionStrings:odindb`: Provided automatically by Aspire when using PostgreSQL

### Aspire AppHost
Redis, PostgreSQL, API, and Web frontend are orchestrated in `AppHost.cs`:
```csharp
var redis = builder.AddRedis("cache");

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin();
var odinDb = postgres.AddDatabase("odindb");

var api = builder.AddProject<Projects.Steinsiek_Odin_API>("api")
    .WithReference(redis)
    .WithReference(odinDb)
    .WaitFor(redis)
    .WaitFor(postgres)
    .WithEnvironment("DatabaseProvider", "PostgreSQL");

builder.AddProject<Projects.Steinsiek_Odin_Web>("web")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints();
```

## Known IDs (Demo Data)

```csharp
// User
Guid.Parse("11111111-1111-1111-1111-111111111111") // demo@steinsiek.de

// Roles
Guid.Parse("aaaa0001-0001-0001-0001-000000000001") // Admin
Guid.Parse("aaaa0001-0001-0001-0001-000000000002") // Manager
Guid.Parse("aaaa0001-0001-0001-0001-000000000003") // User
Guid.Parse("aaaa0001-0001-0001-0001-000000000004") // ReadOnly

// Persons
Guid.Parse("22222222-0001-0001-0001-000000000001") // Max Mustermann
Guid.Parse("22222222-0001-0001-0001-000000000002") // Jane Doe

// Companies
Guid.Parse("33333333-0001-0001-0001-000000000001") // Steinsiek GmbH
Guid.Parse("33333333-0001-0001-0001-000000000002") // Odin Consulting Ltd

// Languages
Guid.Parse("00000001-0001-0001-0001-000000000001") // de (Deutsch)
Guid.Parse("00000001-0001-0001-0001-000000000002") // en (English)

// Lookups (pattern: 000000XX-0001-0001-0001-00000000000Y)
// Salutations: 10, Genders: 20, MaritalStatuses: 30, Countries: 40
// AddressTypes: 50, ContactTypes: 60, Industries: 70, LegalForms: 80
// Departments: 90, Positions: a0
```
