using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Fine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "FINEAMOUNT",
                table: "DELIVERY_COMPLAINTS",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FINEAMOUNT",
                table: "DELIVERY_COMPLAINTS");

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
