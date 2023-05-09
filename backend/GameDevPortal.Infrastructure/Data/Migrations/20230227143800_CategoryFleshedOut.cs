using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryFleshedOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ProgressReports_ProgressReportId",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ProgressReportId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ProgressReportId",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CategoryProgressReport",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgressReportsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProgressReport", x => new { x.CategoriesId, x.ProgressReportsId });
                    table.ForeignKey(
                        name: "FK_CategoryProgressReport_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProgressReport_ProgressReports_ProgressReportsId",
                        column: x => x.ProgressReportsId,
                        principalTable: "ProgressReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProjectId",
                table: "Categories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProgressReport_ProgressReportsId",
                table: "CategoryProgressReport",
                column: "ProgressReportsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Projects_ProjectId",
                table: "Categories",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Projects_ProjectId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryProgressReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProjectId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgressReportId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ProgressReportId",
                table: "Category",
                column: "ProgressReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ProgressReports_ProgressReportId",
                table: "Category",
                column: "ProgressReportId",
                principalTable: "ProgressReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
