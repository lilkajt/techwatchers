using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Techwatchers.Server.Migrations
{
    /// <inheritdoc />
    public partial class addSampleUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "username", "email", "password", "statute" },
                values: new object[,]
                {
                    { "maciej zegarmistrz", "user1@example.com", "passwordHash1", true },
                    { "michał213", "user2@example.com", "passwordHash2", true },
                    { "olek2323", "user3@example.com", "passwordHash3", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValues: new object[] { 5, 6, 7 });
        }
    }
}
