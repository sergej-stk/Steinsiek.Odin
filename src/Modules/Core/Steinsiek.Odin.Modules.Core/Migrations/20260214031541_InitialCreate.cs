using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Steinsiek.Odin.Modules.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.EnsureSchema(
                name: "companies");

            migrationBuilder.EnsureSchema(
                name: "persons");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.CreateTable(
                name: "AddressTypes",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PropertyName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    OldValue = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    NewValue = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LegalFormId = table.Column<Guid>(type: "uuid", nullable: true),
                    IndustryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Website = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TaxNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    VatId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CommercialRegisterNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FoundingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmployeeCount = table.Column<int>(type: "integer", nullable: true),
                    Revenue = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    ParentCompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Companies_ParentCompanyId",
                        column: x => x.ParentCompanyId,
                        principalSchema: "companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalForms",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SalutationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    GenderId = table.Column<Guid>(type: "uuid", nullable: true),
                    NationalityId = table.Column<Guid>(type: "uuid", nullable: true),
                    MaritalStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    Notes = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salutations",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salutations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EntityType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LanguageCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Value = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PreferredLanguage = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValue: "en"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyImages",
                schema: "companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyImages", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyImages_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLocations",
                schema: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Street2 = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    City = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Phone = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLocations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonCompanies",
                schema: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Street2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonBankAccounts",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Iban = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    Bic = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    BankName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonBankAccounts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonEmailAddresses",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonEmailAddresses_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonImages",
                schema: "persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonImages", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonImages_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonLanguages",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProficiencyLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonLanguages_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhoneNumbers",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ContactTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonPhoneNumbers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonSocialMediaLinks",
                schema: "persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Platform = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSocialMediaLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonSocialMediaLinks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "persons",
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "AddressTypes",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000050-0001-0001-0001-000000000001"), "private", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000050-0001-0001-0001-000000000002"), "business", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000050-0001-0001-0001-000000000003"), "delivery", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null }
                });

            migrationBuilder.InsertData(
                schema: "companies",
                table: "Companies",
                columns: new[] { "Id", "CommercialRegisterNumber", "CreatedAt", "DeletedAt", "Email", "EmployeeCount", "FoundingDate", "IndustryId", "IsDeleted", "LegalFormId", "Name", "Notes", "ParentCompanyId", "Phone", "Revenue", "TaxNumber", "UpdatedAt", "VatId", "Website" },
                values: new object[,]
                {
                    { new Guid("33333333-0001-0001-0001-000000000001"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000070-0001-0001-0001-000000000001"), false, new Guid("00000080-0001-0001-0001-000000000001"), "Steinsiek GmbH", null, null, null, null, null, null, null, "https://steinsiek.de" },
                    { new Guid("33333333-0001-0001-0001-000000000002"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, new DateTime(2018, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000070-0001-0001-0001-000000000003"), false, new Guid("00000080-0001-0001-0001-000000000004"), "Odin Consulting Ltd", null, null, null, null, null, null, null, "https://odin-consulting.com" }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "ContactTypes",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000060-0001-0001-0001-000000000001"), "mobile", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000060-0001-0001-0001-000000000002"), "landline", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000060-0001-0001-0001-000000000003"), "fax", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000040-0001-0001-0001-000000000001"), "DE", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000040-0001-0001-0001-000000000002"), "AT", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000040-0001-0001-0001-000000000003"), "CH", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null },
                    { new Guid("00000040-0001-0001-0001-000000000004"), "GB", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4, null },
                    { new Guid("00000040-0001-0001-0001-000000000005"), "US", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 5, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Departments",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000090-0001-0001-0001-000000000001"), "sales", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000090-0001-0001-0001-000000000002"), "it", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000090-0001-0001-0001-000000000003"), "hr", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null },
                    { new Guid("00000090-0001-0001-0001-000000000004"), "finance_dept", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4, null },
                    { new Guid("00000090-0001-0001-0001-000000000005"), "marketing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 5, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Genders",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000020-0001-0001-0001-000000000001"), "male", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000020-0001-0001-0001-000000000002"), "female", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000020-0001-0001-0001-000000000003"), "non_binary", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Industries",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000070-0001-0001-0001-000000000001"), "technology", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000070-0001-0001-0001-000000000002"), "finance", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000070-0001-0001-0001-000000000003"), "consulting", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null },
                    { new Guid("00000070-0001-0001-0001-000000000004"), "manufacturing", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4, null },
                    { new Guid("00000070-0001-0001-0001-000000000005"), "healthcare", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 5, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Languages",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDefault", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000001-0001-0001-0001-000000000001"), "de", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, true, false, "Deutsch", null },
                    { new Guid("00000001-0001-0001-0001-000000000002"), "en", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, false, "English", null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "LegalForms",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000080-0001-0001-0001-000000000001"), "gmbh", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000080-0001-0001-0001-000000000002"), "ag", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000080-0001-0001-0001-000000000003"), "ug", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null },
                    { new Guid("00000080-0001-0001-0001-000000000004"), "ltd", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4, null },
                    { new Guid("00000080-0001-0001-0001-000000000005"), "einzelunternehmen", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 5, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "MaritalStatuses",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000030-0001-0001-0001-000000000001"), "single", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000030-0001-0001-0001-000000000002"), "married", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000030-0001-0001-0001-000000000003"), "divorced", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null }
                });

            migrationBuilder.InsertData(
                schema: "persons",
                table: "Persons",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "DeletedAt", "FirstName", "GenderId", "IsDeleted", "LastName", "MaritalStatusId", "NationalityId", "Notes", "SalutationId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("22222222-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, "Max", new Guid("00000020-0001-0001-0001-000000000001"), false, "Mustermann", null, new Guid("00000040-0001-0001-0001-000000000001"), null, new Guid("00000010-0001-0001-0001-000000000001"), null, null },
                    { new Guid("22222222-0001-0001-0001-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1985, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Jane", new Guid("00000020-0001-0001-0001-000000000002"), false, "Doe", null, new Guid("00000040-0001-0001-0001-000000000004"), null, new Guid("00000010-0001-0001-0001-000000000002"), null, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Positions",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("000000a0-0001-0001-0001-000000000001"), "ceo", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("000000a0-0001-0001-0001-000000000002"), "manager", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("000000a0-0001-0001-0001-000000000003"), "developer", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null },
                    { new Guid("000000a0-0001-0001-0001-000000000004"), "consultant", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4, null },
                    { new Guid("000000a0-0001-0001-0001-000000000005"), "assistant", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 5, null }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaa0001-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Full access including audit log, user management and roles", false, "Admin", null },
                    { new Guid("aaaa0001-0001-0001-0001-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "CRUD access to persons and companies", false, "Manager", null },
                    { new Guid("aaaa0001-0001-0001-0001-000000000003"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Limited CRUD access to persons and companies", false, "User", null },
                    { new Guid("aaaa0001-0001-0001-0001-000000000004"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Read-only access to all data", false, "ReadOnly", null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Salutations",
                columns: new[] { "Id", "Code", "CreatedAt", "DeletedAt", "IsDeleted", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000010-0001-0001-0001-000000000001"), "mr", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1, null },
                    { new Guid("00000010-0001-0001-0001-000000000002"), "mrs", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2, null },
                    { new Guid("00000010-0001-0001-0001-000000000003"), "mx", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3, null }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Translations",
                columns: new[] { "Id", "EntityId", "EntityType", "LanguageCode", "Value" },
                values: new object[,]
                {
                    { new Guid("00000010-0002-0001-0001-000000000001"), new Guid("00000010-0001-0001-0001-000000000001"), "Salutation", "de", "Herr" },
                    { new Guid("00000010-0002-0001-0001-000000000002"), new Guid("00000010-0001-0001-0001-000000000001"), "Salutation", "en", "Mr" },
                    { new Guid("00000010-0002-0001-0001-000000000003"), new Guid("00000010-0001-0001-0001-000000000002"), "Salutation", "de", "Frau" },
                    { new Guid("00000010-0002-0001-0001-000000000004"), new Guid("00000010-0001-0001-0001-000000000002"), "Salutation", "en", "Mrs" },
                    { new Guid("00000010-0002-0001-0001-000000000005"), new Guid("00000010-0001-0001-0001-000000000003"), "Salutation", "de", "Divers" },
                    { new Guid("00000010-0002-0001-0001-000000000006"), new Guid("00000010-0001-0001-0001-000000000003"), "Salutation", "en", "Mx" },
                    { new Guid("00000020-0002-0001-0001-000000000001"), new Guid("00000020-0001-0001-0001-000000000001"), "Gender", "de", "Maennlich" },
                    { new Guid("00000020-0002-0001-0001-000000000002"), new Guid("00000020-0001-0001-0001-000000000001"), "Gender", "en", "Male" },
                    { new Guid("00000020-0002-0001-0001-000000000003"), new Guid("00000020-0001-0001-0001-000000000002"), "Gender", "de", "Weiblich" },
                    { new Guid("00000020-0002-0001-0001-000000000004"), new Guid("00000020-0001-0001-0001-000000000002"), "Gender", "en", "Female" },
                    { new Guid("00000020-0002-0001-0001-000000000005"), new Guid("00000020-0001-0001-0001-000000000003"), "Gender", "de", "Divers" },
                    { new Guid("00000020-0002-0001-0001-000000000006"), new Guid("00000020-0001-0001-0001-000000000003"), "Gender", "en", "Non-binary" },
                    { new Guid("00000030-0002-0001-0001-000000000001"), new Guid("00000030-0001-0001-0001-000000000001"), "MaritalStatus", "de", "Ledig" },
                    { new Guid("00000030-0002-0001-0001-000000000002"), new Guid("00000030-0001-0001-0001-000000000001"), "MaritalStatus", "en", "Single" },
                    { new Guid("00000030-0002-0001-0001-000000000003"), new Guid("00000030-0001-0001-0001-000000000002"), "MaritalStatus", "de", "Verheiratet" },
                    { new Guid("00000030-0002-0001-0001-000000000004"), new Guid("00000030-0001-0001-0001-000000000002"), "MaritalStatus", "en", "Married" },
                    { new Guid("00000030-0002-0001-0001-000000000005"), new Guid("00000030-0001-0001-0001-000000000003"), "MaritalStatus", "de", "Geschieden" },
                    { new Guid("00000030-0002-0001-0001-000000000006"), new Guid("00000030-0001-0001-0001-000000000003"), "MaritalStatus", "en", "Divorced" },
                    { new Guid("00000040-0002-0001-0001-000000000001"), new Guid("00000040-0001-0001-0001-000000000001"), "Country", "de", "Deutschland" },
                    { new Guid("00000040-0002-0001-0001-000000000002"), new Guid("00000040-0001-0001-0001-000000000001"), "Country", "en", "Germany" },
                    { new Guid("00000040-0002-0001-0001-000000000003"), new Guid("00000040-0001-0001-0001-000000000002"), "Country", "de", "Oesterreich" },
                    { new Guid("00000040-0002-0001-0001-000000000004"), new Guid("00000040-0001-0001-0001-000000000002"), "Country", "en", "Austria" },
                    { new Guid("00000040-0002-0001-0001-000000000005"), new Guid("00000040-0001-0001-0001-000000000003"), "Country", "de", "Schweiz" },
                    { new Guid("00000040-0002-0001-0001-000000000006"), new Guid("00000040-0001-0001-0001-000000000003"), "Country", "en", "Switzerland" },
                    { new Guid("00000040-0002-0001-0001-000000000007"), new Guid("00000040-0001-0001-0001-000000000004"), "Country", "de", "Vereinigtes Koenigreich" },
                    { new Guid("00000040-0002-0001-0001-000000000008"), new Guid("00000040-0001-0001-0001-000000000004"), "Country", "en", "United Kingdom" },
                    { new Guid("00000040-0002-0001-0001-000000000009"), new Guid("00000040-0001-0001-0001-000000000005"), "Country", "de", "Vereinigte Staaten" },
                    { new Guid("00000040-0002-0001-0001-000000000010"), new Guid("00000040-0001-0001-0001-000000000005"), "Country", "en", "United States" },
                    { new Guid("00000050-0002-0001-0001-000000000001"), new Guid("00000050-0001-0001-0001-000000000001"), "AddressType", "de", "Privat" },
                    { new Guid("00000050-0002-0001-0001-000000000002"), new Guid("00000050-0001-0001-0001-000000000001"), "AddressType", "en", "Private" },
                    { new Guid("00000050-0002-0001-0001-000000000003"), new Guid("00000050-0001-0001-0001-000000000002"), "AddressType", "de", "Geschaeftlich" },
                    { new Guid("00000050-0002-0001-0001-000000000004"), new Guid("00000050-0001-0001-0001-000000000002"), "AddressType", "en", "Business" },
                    { new Guid("00000050-0002-0001-0001-000000000005"), new Guid("00000050-0001-0001-0001-000000000003"), "AddressType", "de", "Lieferung" },
                    { new Guid("00000050-0002-0001-0001-000000000006"), new Guid("00000050-0001-0001-0001-000000000003"), "AddressType", "en", "Delivery" },
                    { new Guid("00000060-0002-0001-0001-000000000001"), new Guid("00000060-0001-0001-0001-000000000001"), "ContactType", "de", "Mobil" },
                    { new Guid("00000060-0002-0001-0001-000000000002"), new Guid("00000060-0001-0001-0001-000000000001"), "ContactType", "en", "Mobile" },
                    { new Guid("00000060-0002-0001-0001-000000000003"), new Guid("00000060-0001-0001-0001-000000000002"), "ContactType", "de", "Festnetz" },
                    { new Guid("00000060-0002-0001-0001-000000000004"), new Guid("00000060-0001-0001-0001-000000000002"), "ContactType", "en", "Landline" },
                    { new Guid("00000060-0002-0001-0001-000000000005"), new Guid("00000060-0001-0001-0001-000000000003"), "ContactType", "de", "Fax" },
                    { new Guid("00000060-0002-0001-0001-000000000006"), new Guid("00000060-0001-0001-0001-000000000003"), "ContactType", "en", "Fax" },
                    { new Guid("00000070-0002-0001-0001-000000000001"), new Guid("00000070-0001-0001-0001-000000000001"), "Industry", "de", "Technologie" },
                    { new Guid("00000070-0002-0001-0001-000000000002"), new Guid("00000070-0001-0001-0001-000000000001"), "Industry", "en", "Technology" },
                    { new Guid("00000070-0002-0001-0001-000000000003"), new Guid("00000070-0001-0001-0001-000000000002"), "Industry", "de", "Finanzen" },
                    { new Guid("00000070-0002-0001-0001-000000000004"), new Guid("00000070-0001-0001-0001-000000000002"), "Industry", "en", "Finance" },
                    { new Guid("00000070-0002-0001-0001-000000000005"), new Guid("00000070-0001-0001-0001-000000000003"), "Industry", "de", "Beratung" },
                    { new Guid("00000070-0002-0001-0001-000000000006"), new Guid("00000070-0001-0001-0001-000000000003"), "Industry", "en", "Consulting" },
                    { new Guid("00000070-0002-0001-0001-000000000007"), new Guid("00000070-0001-0001-0001-000000000004"), "Industry", "de", "Produktion" },
                    { new Guid("00000070-0002-0001-0001-000000000008"), new Guid("00000070-0001-0001-0001-000000000004"), "Industry", "en", "Manufacturing" },
                    { new Guid("00000070-0002-0001-0001-000000000009"), new Guid("00000070-0001-0001-0001-000000000005"), "Industry", "de", "Gesundheitswesen" },
                    { new Guid("00000070-0002-0001-0001-000000000010"), new Guid("00000070-0001-0001-0001-000000000005"), "Industry", "en", "Healthcare" },
                    { new Guid("00000080-0002-0001-0001-000000000001"), new Guid("00000080-0001-0001-0001-000000000001"), "LegalForm", "de", "GmbH" },
                    { new Guid("00000080-0002-0001-0001-000000000002"), new Guid("00000080-0001-0001-0001-000000000001"), "LegalForm", "en", "Ltd" },
                    { new Guid("00000080-0002-0001-0001-000000000003"), new Guid("00000080-0001-0001-0001-000000000002"), "LegalForm", "de", "AG" },
                    { new Guid("00000080-0002-0001-0001-000000000004"), new Guid("00000080-0001-0001-0001-000000000002"), "LegalForm", "en", "Corp" },
                    { new Guid("00000080-0002-0001-0001-000000000005"), new Guid("00000080-0001-0001-0001-000000000003"), "LegalForm", "de", "UG" },
                    { new Guid("00000080-0002-0001-0001-000000000006"), new Guid("00000080-0001-0001-0001-000000000003"), "LegalForm", "en", "Ltd (limited)" },
                    { new Guid("00000080-0002-0001-0001-000000000007"), new Guid("00000080-0001-0001-0001-000000000004"), "LegalForm", "de", "Ltd" },
                    { new Guid("00000080-0002-0001-0001-000000000008"), new Guid("00000080-0001-0001-0001-000000000004"), "LegalForm", "en", "Ltd" },
                    { new Guid("00000080-0002-0001-0001-000000000009"), new Guid("00000080-0001-0001-0001-000000000005"), "LegalForm", "de", "Einzelunternehmen" },
                    { new Guid("00000080-0002-0001-0001-000000000010"), new Guid("00000080-0001-0001-0001-000000000005"), "LegalForm", "en", "Sole Proprietorship" },
                    { new Guid("00000090-0002-0001-0001-000000000001"), new Guid("00000090-0001-0001-0001-000000000001"), "Department", "de", "Vertrieb" },
                    { new Guid("00000090-0002-0001-0001-000000000002"), new Guid("00000090-0001-0001-0001-000000000001"), "Department", "en", "Sales" },
                    { new Guid("00000090-0002-0001-0001-000000000003"), new Guid("00000090-0001-0001-0001-000000000002"), "Department", "de", "IT" },
                    { new Guid("00000090-0002-0001-0001-000000000004"), new Guid("00000090-0001-0001-0001-000000000002"), "Department", "en", "IT" },
                    { new Guid("00000090-0002-0001-0001-000000000005"), new Guid("00000090-0001-0001-0001-000000000003"), "Department", "de", "Personal" },
                    { new Guid("00000090-0002-0001-0001-000000000006"), new Guid("00000090-0001-0001-0001-000000000003"), "Department", "en", "HR" },
                    { new Guid("00000090-0002-0001-0001-000000000007"), new Guid("00000090-0001-0001-0001-000000000004"), "Department", "de", "Finanzen" },
                    { new Guid("00000090-0002-0001-0001-000000000008"), new Guid("00000090-0001-0001-0001-000000000004"), "Department", "en", "Finance" },
                    { new Guid("00000090-0002-0001-0001-000000000009"), new Guid("00000090-0001-0001-0001-000000000005"), "Department", "de", "Marketing" },
                    { new Guid("00000090-0002-0001-0001-000000000010"), new Guid("00000090-0001-0001-0001-000000000005"), "Department", "en", "Marketing" },
                    { new Guid("000000a0-0002-0001-0001-000000000001"), new Guid("000000a0-0001-0001-0001-000000000001"), "Position", "de", "Geschaeftsfuehrer" },
                    { new Guid("000000a0-0002-0001-0001-000000000002"), new Guid("000000a0-0001-0001-0001-000000000001"), "Position", "en", "CEO" },
                    { new Guid("000000a0-0002-0001-0001-000000000003"), new Guid("000000a0-0001-0001-0001-000000000002"), "Position", "de", "Manager" },
                    { new Guid("000000a0-0002-0001-0001-000000000004"), new Guid("000000a0-0001-0001-0001-000000000002"), "Position", "en", "Manager" },
                    { new Guid("000000a0-0002-0001-0001-000000000005"), new Guid("000000a0-0001-0001-0001-000000000003"), "Position", "de", "Entwickler" },
                    { new Guid("000000a0-0002-0001-0001-000000000006"), new Guid("000000a0-0001-0001-0001-000000000003"), "Position", "en", "Developer" },
                    { new Guid("000000a0-0002-0001-0001-000000000007"), new Guid("000000a0-0001-0001-0001-000000000004"), "Position", "de", "Berater" },
                    { new Guid("000000a0-0002-0001-0001-000000000008"), new Guid("000000a0-0001-0001-0001-000000000004"), "Position", "en", "Consultant" },
                    { new Guid("000000a0-0002-0001-0001-000000000009"), new Guid("000000a0-0001-0001-0001-000000000005"), "Position", "de", "Assistent" },
                    { new Guid("000000a0-0002-0001-0001-000000000010"), new Guid("000000a0-0001-0001-0001-000000000005"), "Position", "en", "Assistant" }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "PasswordHash", "PreferredLanguage", "UpdatedAt" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "demo@steinsiek.de", "Demo", true, false, "User", "$2a$11$.eVWCuc9QF3UHNOeQX9sAuDmkRj2MYyZvTFbsLjlfvvz7711qoFOK", "en", null });

            migrationBuilder.InsertData(
                schema: "companies",
                table: "CompanyLocations",
                columns: new[] { "Id", "City", "CompanyId", "CountryId", "CreatedAt", "DeletedAt", "Email", "IsDeleted", "IsPrimary", "Latitude", "Longitude", "Name", "Phone", "PostalCode", "State", "Street", "Street2", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-0002-0001-0001-000000000001"), "Hamburg", new Guid("33333333-0001-0001-0001-000000000001"), new Guid("00000040-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, null, null, "Headquarters", null, "20095", null, "Musterstraße 1", null, null },
                    { new Guid("33333333-0002-0001-0001-000000000002"), "London", new Guid("33333333-0001-0001-0001-000000000002"), new Guid("00000040-0001-0001-0001-000000000004"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, false, true, null, null, "Main Office", null, "SW1A 2AA", null, "10 Downing Street", null, null }
                });

            migrationBuilder.InsertData(
                schema: "persons",
                table: "PersonAddresses",
                columns: new[] { "Id", "AddressTypeId", "City", "CountryId", "CreatedAt", "DeletedAt", "IsDeleted", "IsPrimary", "PersonId", "PostalCode", "State", "Street", "Street2", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-0001-0001-0001-000000000001"), null, "Berlin", new Guid("00000040-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, true, new Guid("22222222-0001-0001-0001-000000000001"), "10115", "Berlin", "Musterstrasse 1", null, null },
                    { new Guid("33333333-0001-0001-0001-000000000002"), null, "New York", new Guid("00000040-0001-0001-0001-000000000004"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, true, new Guid("22222222-0001-0001-0001-000000000002"), "10001", "NY", "123 Main Street", "Apt 4B", null }
                });

            migrationBuilder.InsertData(
                schema: "companies",
                table: "PersonCompanies",
                columns: new[] { "Id", "CompanyId", "CreatedAt", "DeletedAt", "DepartmentId", "EndDate", "IsActive", "IsDeleted", "PersonId", "PositionId", "StartDate", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("33333333-0003-0001-0001-000000000001"), new Guid("33333333-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new Guid("00000090-0001-0001-0001-000000000002"), null, true, false, new Guid("22222222-0001-0001-0001-000000000001"), new Guid("000000a0-0001-0001-0001-000000000001"), new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("33333333-0003-0001-0001-000000000002"), new Guid("33333333-0001-0001-0001-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new Guid("00000090-0001-0001-0001-000000000001"), null, true, false, new Guid("22222222-0001-0001-0001-000000000002"), new Guid("000000a0-0001-0001-0001-000000000002"), new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.InsertData(
                schema: "persons",
                table: "PersonEmailAddresses",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "IsDeleted", "IsPrimary", "Label", "PersonId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("44444444-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "max@mustermann.de", false, true, "Personal", new Guid("22222222-0001-0001-0001-000000000001"), null },
                    { new Guid("44444444-0001-0001-0001-000000000002"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "jane@doe.com", false, true, "Personal", new Guid("22222222-0001-0001-0001-000000000002"), null }
                });

            migrationBuilder.InsertData(
                schema: "persons",
                table: "PersonPhoneNumbers",
                columns: new[] { "Id", "ContactTypeId", "CreatedAt", "DeletedAt", "IsDeleted", "IsPrimary", "Label", "Number", "PersonId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("55555555-0001-0001-0001-000000000001"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, true, "Mobile", "+49 170 1234567", new Guid("22222222-0001-0001-0001-000000000001"), null },
                    { new Guid("55555555-0001-0001-0001-000000000002"), null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, true, "Mobile", "+1 212 5551234", new Guid("22222222-0001-0001-0001-000000000002"), null }
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "RoleId", "UpdatedAt", "UserId" },
                values: new object[] { new Guid("aaaa0002-0001-0001-0001-000000000001"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aaaa0001-0001-0001-0001-000000000001"), null, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.CreateIndex(
                name: "IX_AddressTypes_Code",
                schema: "core",
                table: "AddressTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_EntityType_EntityId",
                schema: "core",
                table: "AuditLog",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId_Timestamp",
                schema: "core",
                table: "AuditLog",
                columns: new[] { "UserId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ParentCompanyId",
                schema: "companies",
                table: "Companies",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLocations_CompanyId",
                schema: "companies",
                table: "CompanyLocations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactTypes_Code",
                schema: "core",
                table: "ContactTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                schema: "core",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Code",
                schema: "core",
                table: "Departments",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Code",
                schema: "core",
                table: "Genders",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Industries_Code",
                schema: "core",
                table: "Industries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                schema: "core",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LegalForms_Code",
                schema: "core",
                table: "LegalForms",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaritalStatuses_Code",
                schema: "core",
                table: "MaritalStatuses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_PersonId",
                schema: "persons",
                table: "PersonAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonBankAccounts_PersonId",
                schema: "persons",
                table: "PersonBankAccounts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanies_CompanyId",
                schema: "companies",
                table: "PersonCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCompanies_PersonId_CompanyId",
                schema: "companies",
                table: "PersonCompanies",
                columns: new[] { "PersonId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmailAddresses_PersonId",
                schema: "persons",
                table: "PersonEmailAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLanguages_PersonId",
                schema: "persons",
                table: "PersonLanguages",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhoneNumbers_PersonId",
                schema: "persons",
                table: "PersonPhoneNumbers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSocialMediaLinks_PersonId",
                schema: "persons",
                table: "PersonSocialMediaLinks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_Code",
                schema: "core",
                table: "Positions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "auth",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salutations_Code",
                schema: "core",
                table: "Salutations",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_EntityId_LanguageCode",
                schema: "core",
                table: "Translations",
                columns: new[] { "EntityId", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Translations_EntityType_LanguageCode",
                schema: "core",
                table: "Translations",
                columns: new[] { "EntityType", "LanguageCode" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "auth",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                schema: "auth",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "auth",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressTypes",
                schema: "core");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "core");

            migrationBuilder.DropTable(
                name: "CompanyImages",
                schema: "companies");

            migrationBuilder.DropTable(
                name: "CompanyLocations",
                schema: "companies");

            migrationBuilder.DropTable(
                name: "ContactTypes",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Industries",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "core");

            migrationBuilder.DropTable(
                name: "LegalForms",
                schema: "core");

            migrationBuilder.DropTable(
                name: "MaritalStatuses",
                schema: "core");

            migrationBuilder.DropTable(
                name: "PersonAddresses",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "PersonBankAccounts",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "PersonCompanies",
                schema: "companies");

            migrationBuilder.DropTable(
                name: "PersonEmailAddresses",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "PersonImages",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "PersonLanguages",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "PersonPhoneNumbers",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "PersonSocialMediaLinks",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Salutations",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Translations",
                schema: "core");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "companies");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "persons");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "auth");
        }
    }
}
