using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EPROJECTMANAGER.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLichPhongVan_AddTenLich : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenLich",
                table: "LichPhongVans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenLich",
                table: "LichPhongVans");
        }
    }
}
