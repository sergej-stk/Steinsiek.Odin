using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steinsiek.Odin.Modules.Core.Migrations
{
    /// <inheritdoc />
    public partial class RenameSeedCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "companies",
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0001-0001-0001-000000000002"),
                columns: new[] { "Name", "Website" },
                values: new object[] { "Steinsiek Consulting Ltd", "https://steinsiek-consulting.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "companies",
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0001-0001-0001-000000000002"),
                columns: new[] { "Name", "Website" },
                values: new object[] { "Odin Consulting Ltd", "https://odin-consulting.com" });
        }
    }
}
