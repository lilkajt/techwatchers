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
            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "name",
                keyValue: "testowaKategoria",
                column: "name",
                value: "żywotność"
            );
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "name" },
                values: new object[,]
                {
                    { "ekrany" },
                    { "naprawa" },
                    { "diagnostyka" },
                    { "prezenty" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "name",
                keyValue: "żywotność",
                column: "name",
                value: "testowaKategoria"
            );
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "name",
                keyValues: new object[]
                {
                    "ekrany",
                    "naprawa",
                    "diagnostyka",
                    "prezenty"
                });
        }
    }
}
