using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProgressReportAndNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_Projects_ProjectId",
                table: "FAQ");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressReport_Projects_ProjectId",
                table: "ProgressReport");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ProgressReport",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProgressReport",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedAt",
                table: "ProgressReport",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ProgressReport",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "FAQ",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HexColour = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    ProgressReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_ProgressReport_ProgressReportId",
                        column: x => x.ProgressReportId,
                        principalTable: "ProgressReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ProgressReportId",
                table: "Category",
                column: "ProgressReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_Projects_ProjectId",
                table: "FAQ",
                column: "ProjectId",
                principalTable: "Projects",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_Projects_ProjectId",
                table: "FAQ");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressReport_Projects_ProjectId",
                table: "ProgressReport");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProgressReport");

            migrationBuilder.DropColumn(
                name: "PostedAt",
                table: "ProgressReport");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ProgressReport");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ProgressReport",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "FAQ",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_Projects_ProjectId",
                table: "FAQ",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressReport_Projects_ProjectId",
                table: "ProgressReport",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
