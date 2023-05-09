using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("22c6bd6c-8ffc-4bd2-8143-5ca03557cd99"), null, "User", "USER" },
                    { new Guid("296ecfda-774c-4042-9eef-6712df53031e"), null, "Admin", "ADMIN" },
                    { new Guid("2cb05251-40f6-44fb-8366-781953860b5d"), null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("22c6bd6c-8ffc-4bd2-8143-5ca03557cd99"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("296ecfda-774c-4042-9eef-6712df53031e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2cb05251-40f6-44fb-8366-781953860b5d"));
        }
    }
}
