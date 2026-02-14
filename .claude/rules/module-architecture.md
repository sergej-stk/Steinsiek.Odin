# Module Architecture

## Module Architecture

Each module's code is split across two submodule repos:

**API submodule** (`src/Steinsiek.Odin.API/Modules/{ModuleName}/`):
```
Steinsiek.Odin.Modules.{ModuleName}/       # Core Class Library
├── {ModuleName}Module.cs                  # IModule Implementation (DI Registration)
├── Controllers/                           # API Endpoints
├── Entities/                              # Domain Entities (inherit from BaseEntity)
├── Persistence/Configurations/            # IEntityTypeConfiguration<T> (Fluent API)
├── Repositories/                          # I{Name}Repository + EF Implementation
└── Services/                              # I{Name}Service + Implementation
Steinsiek.Odin.Modules.{ModuleName}.Tests/ # Unit Tests
```

**Shared submodule** (`src/Steinsiek.Odin.Shared/`):
```
Steinsiek.Odin.Modules.{ModuleName}.Shared/    # Shared Contracts
└── DTOs/                                      # Request/Response Records
```

### Module Responsibility Rules

**Hard architectural constraints** that must be followed:

| Project | Contains | Does NOT Contain |
|---------|----------|------------------|
| `{ModuleName}` | Controllers, Services, Repositories, Entities, Entity Configurations, Application logic | DTOs, Shared contracts |
| `{ModuleName}.Shared` | DTOs, Contracts, Interfaces, Enums, Value objects, Constants | Controllers, Services, Business logic |
| `{ModuleName}.Tests` | Unit tests, Integration tests | Business logic, Production code |

**Failure Conditions:**
- Logic in `.Shared` is incorrect
- DTOs outside `.Shared` are incorrect
- Cross-module dependencies are incorrect
- Tests referencing other modules (except their own `.Shared`) is incorrect

### IModule Interface

Each module implements `IModule` from the Core module:

```csharp
public sealed class AuthModule : IModule
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
    }
}
```

Registration happens centrally in `ServiceCollectionExtensions.cs`:

```csharp
public static IServiceCollection AddModules(this IServiceCollection services)
{
    AuthModule.RegisterServices(services);
    PersonsModule.RegisterServices(services);
    CompaniesModule.RegisterServices(services);
    return services;
}
```

## Adding a New Module

1. Create module projects in the API submodule:
```bash
cd src/Steinsiek.Odin.API/Modules
mkdir {Name}
cd {Name}

# Create module projects
dotnet new classlib -n Steinsiek.Odin.Modules.{Name}
dotnet new mstest -n Steinsiek.Odin.Modules.{Name}.Tests
```

2. Create Shared project in the Shared submodule:
```bash
cd src/Steinsiek.Odin.Shared
dotnet new classlib -n Steinsiek.Odin.Modules.{Name}.Shared
```

3. Add to main solution (`Steinsiek.Odin.slnx`):
```bash
cd C:\Development\Steinsiek.Odin
dotnet sln add src/Steinsiek.Odin.API/Modules/{Name}/Steinsiek.Odin.Modules.{Name}/Steinsiek.Odin.Modules.{Name}.csproj
dotnet sln add src/Steinsiek.Odin.API/Modules/{Name}/Steinsiek.Odin.Modules.{Name}.Tests/Steinsiek.Odin.Modules.{Name}.Tests.csproj
dotnet sln add src/Steinsiek.Odin.Shared/Steinsiek.Odin.Modules.{Name}.Shared/Steinsiek.Odin.Modules.{Name}.Shared.csproj
```

4. Add project references (paths relative to each .csproj):
```bash
# Module references Core (within API submodule) and its own Shared (in Shared submodule)
# Edit .csproj directly — ProjectReference paths cross submodule boundaries
```

4. Update module `.csproj` with ASP.NET Core framework reference:
```xml
<ItemGroup>
  <FrameworkReference Include="Microsoft.AspNetCore.App" />
</ItemGroup>
```

5. Update `.Shared.csproj` with annotations package:
```xml
<ItemGroup>
  <PackageReference Include="System.ComponentModel.Annotations" Version="5.0.0" />
</ItemGroup>
```

6. Create `GlobalUsings.cs` in each project with required namespaces

7. Create `{Name}Module.cs` implementing `IModule`

8. Create DTOs in `.Shared/DTOs/` folder

9. Register in `ServiceCollectionExtensions.cs`

10. Add assembly in `Program.cs`:
```csharp
.AddApplicationPart(typeof(Steinsiek.Odin.Modules.{Name}.{Name}Module).Assembly)
```

11. Create entity type configurations in `Persistence/Configurations/` folder

12. Register module assembly in `ServiceCollectionExtensions.AddDatabase()`:
```csharp
options.ModuleAssemblies.Add(typeof(Steinsiek.Odin.Modules.{Name}.{Name}Module).Assembly);
```

13. Generate migration:
```bash
dotnet ef migrations add Add{Name}Module --project src/Steinsiek.Odin.API/Modules/Core/Steinsiek.Odin.Modules.Core --startup-project src/Steinsiek.Odin.API/Steinsiek.Odin.API --output-dir Migrations
```
