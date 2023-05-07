using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("094bf682-2c34-4d18-a24f-a20ffbd232be"), null, "Moderator", "MODERATOR" },
                    { new Guid("27a71320-dfd4-4833-bbad-5f7fc5670532"), null, "User", "USER" },
                    { new Guid("662ce9ac-052d-4211-ae0d-50883ccdf872"), null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("094bf682-2c34-4d18-a24f-a20ffbd232be"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("27a71320-dfd4-4833-bbad-5f7fc5670532"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("662ce9ac-052d-4211-ae0d-50883ccdf872"));

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
    }
}
