# Frontend Architecture

## Frontend Architecture

### Overview
The frontend is a **Blazor Web App** (`Steinsiek.Odin.Web`) with **Interactive Server** rendering mode. It uses **Bootstrap 5.3** for UI styling.

### Project Structure
```
src/Steinsiek.Odin.Web/
├── Program.cs                          # Entry point, DI, Serilog, Bootstrap
├── GlobalUsings.cs                     # Global using directives
├── Resources/                          # Localization (.resx files, EN + DE)
│   ├── SharedResources.cs              # Marker class
│   ├── SharedResources.resx            # English (default)
│   └── SharedResources.de.resx         # German
├── Auth/                               # JWT auth state management
│   ├── ITokenStorageService.cs
│   ├── TokenStorageService.cs          # ProtectedSessionStorage-based
│   └── JwtAuthenticationStateProvider.cs
├── Services/                           # Typed HttpClient API clients + Toast
│   ├── ApiAuthenticationHandler.cs     # DelegatingHandler for JWT
│   ├── IAuthApiClient.cs + AuthApiClient.cs
│   ├── IPersonApiClient.cs + PersonApiClient.cs
│   ├── ICompanyApiClient.cs + CompanyApiClient.cs
│   ├── IDashboardApiClient.cs + DashboardApiClient.cs
│   ├── IToastService.cs + ToastService.cs  # Toast notification service
│   ├── ToastLevel.cs                   # Toast severity enum
│   └── ToastMessage.cs                 # Toast message model
├── Components/
│   ├── App.razor                       # Root HTML component
│   ├── Routes.razor                    # Router with AuthorizeRouteView
│   ├── _Imports.razor                  # Global Razor imports
│   ├── Layout/                         # MainLayout, NavMenu
│   ├── Pages/
│   │   ├── Home.razor                  # Dashboard with stats, activity, birthdays
│   │   ├── Persons/                    # PersonList, PersonDetail, PersonCreate, PersonEdit
│   │   └── Companies/                  # CompanyList, CompanyDetail, CompanyCreate, CompanyEdit
│   ├── Auth/                           # Auth form components
│   └── Shared/                         # Reusable components (DataGrid, PageHeader, etc.)
└── wwwroot/                            # Static assets (CSS, JS interop)
```

### API Communication
- Typed `HttpClient` services with Aspire service discovery (`https+http://api`)
- References `.Shared` projects for DTO types (no duplication)
- `ApiAuthenticationHandler` attaches JWT Bearer token to all outgoing requests
- The Web project does NOT reference module core projects (only `.Shared`)

### Authentication Flow
1. User submits credentials via `LoginForm`
2. `AuthApiClient` calls `POST /api/v1/auth/login`
3. JWT token stored in `ProtectedSessionStorage` (server-side encrypted)
4. `JwtAuthenticationStateProvider` parses JWT claims for `AuthenticationState`
5. `<AuthorizeView>` and `@attribute [Authorize]` control UI visibility

### Frontend Conventions
- Blazor components use `_Imports.razor` for Razor-scoped imports
- C# code-behind files use `GlobalUsings.cs` (same as backend)
- API client services follow interface-first pattern (`I{Name}ApiClient`)
- All service implementations are `sealed`
- Pages use `@attribute [Authorize]` for protected routes
- No business logic in components — all API interaction through services
- Custom `IToastService` for user notifications (`ToastService.Show(message, ToastLevel)`)
- Bootstrap Modals for confirmation and form dialogs (declarative inline components with `Visible` parameter)
- Forms use `EditForm` + `DataAnnotationsValidator` for form validation
- Icons use Bootstrap Icons (`bi-*` classes)

### Theme
- CSS Custom Properties for dark/light mode (`data-theme` attribute toggle)
- Bootstrap standard color scheme (Blue/Grey), Inter font
- Dark mode toggle in navbar
