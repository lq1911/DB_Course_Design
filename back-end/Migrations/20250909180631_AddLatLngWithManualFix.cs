using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddLatLngWithManualFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- 原有的 RenameColumn 代码已被删除 ---

            // --- 替换为正确的 AddColumn 操作 ---
            migrationBuilder.AddColumn<decimal>(
                name: "LATITUDE",
                table: "STORES",
                type: "decimal(10,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LONGITUDE",
                table: "STORES",
                type: "decimal(10,6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // --- 原有的 RenameColumn 代码已被删除 ---

            // --- 替换为与 Up() 方法相反的 DropColumn 操作 ---
            migrationBuilder.DropColumn(
                name: "LATITUDE",
                table: "STORES");

            migrationBuilder.DropColumn(
                name: "LONGITUDE",
                table: "STORES");
        }
    }
}