using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFaq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_Projects_ProjectId",
                table: "FAQ");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FAQ",
                table: "FAQ");

            migrationBuilder.RenameTable(
                name: "FAQ",
                newName: "Faqs");

            migrationBuilder.RenameIndex(
                name: "IX_FAQ_ProjectId",
                table: "Faqs",
                newName: "IX_Faqs_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faqs",
                table: "Faqs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Projects_ProjectId",
                table: "Faqs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Projects_ProjectId",
                table: "Faqs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faqs",
                table: "Faqs");

            migrationBuilder.RenameTable(
                name: "Faqs",
                newName: "FAQ");

            migrationBuilder.RenameIndex(
                name: "IX_Faqs_ProjectId",
                table: "FAQ",
                newName: "IX_FAQ_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FAQ",
                table: "FAQ",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_Projects_ProjectId",
                table: "FAQ",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
