using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProgressReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ProgressReport_ProgressReportId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressReport_Projects_ProjectId",
                table: "ProgressReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressReport",
                table: "ProgressReport");

            migrationBuilder.RenameTable(
                name: "ProgressReport",
                newName: "ProgressReports");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressReport_ProjectId",
                table: "ProgressReports",
                newName: "IX_ProgressReports_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressReports",
                table: "ProgressReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ProgressReports_ProgressReportId",
                table: "Category",
                column: "ProgressReportId",
                principalTable: "ProgressReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressReports_Projects_ProjectId",
                table: "ProgressReports",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_ProgressReports_ProgressReportId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressReports_Projects_ProjectId",
                table: "ProgressReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressReports",
                table: "ProgressReports");

            migrationBuilder.RenameTable(
                name: "ProgressReports",
                newName: "ProgressReport");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressReports_ProjectId",
                table: "ProgressReport",
                newName: "IX_ProgressReport_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressReport",
                table: "ProgressReport",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_ProgressReport_ProgressReportId",
                table: "Category",
                column: "ProgressReportId",
                principalTable: "ProgressReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressReport_Projects_ProjectId",
                table: "ProgressReport",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
