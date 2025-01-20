using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Techwatchers.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "name" },
                values: new object[,]
                {
                    { "konfiguracja" },
                    { "ekrany" },
                    { "naprawa" },
                    { "diagnostyka" },
                    { "prezenty" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "name",
                keyValues: new object[]
                {
                    "konfiguracja",
                    "ekrany",
                    "naprawa",
                    "diagnostyka",
                    "prezenty"
                });
        }
    }
}
