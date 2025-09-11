using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStoreLatLngToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "STORES",
                type: "decimal(10,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "STORES",
                type: "decimal(10,6)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SHOPPINGCARTSTATE",
                table: "SHOPPING_CARTS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "STORES");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "STORES");

            migrationBuilder.AlterColumn<string>(
                name: "SHOPPINGCARTSTATE",
                table: "SHOPPING_CARTS",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
