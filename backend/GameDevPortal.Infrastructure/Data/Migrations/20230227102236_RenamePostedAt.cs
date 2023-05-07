using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDevPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePostedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostedAt",
                table: "ProgressReports",
                newName: "MadePublicAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MadePublicAt",
                table: "ProgressReports",
                newName: "PostedAt");
        }
    }
}
