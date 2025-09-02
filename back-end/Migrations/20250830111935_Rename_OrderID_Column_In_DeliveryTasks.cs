using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Rename_OrderID_Column_In_DeliveryTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DELIVERY_TASKS_FOOD_ORDERS_OrderID",
                table: "DELIVERY_TASKS");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "DELIVERY_TASKS",
                newName: "ORDERID");

            migrationBuilder.RenameIndex(
                name: "IX_DELIVERY_TASKS_OrderID",
                table: "DELIVERY_TASKS",
                newName: "IX_DELIVERY_TASKS_ORDERID");

            migrationBuilder.AddForeignKey(
                name: "FK_DELIVERY_TASKS_FOOD_ORDERS_ORDERID",
                table: "DELIVERY_TASKS",
                column: "ORDERID",
                principalTable: "FOOD_ORDERS",
                principalColumn: "ORDERID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DELIVERY_TASKS_FOOD_ORDERS_ORDERID",
                table: "DELIVERY_TASKS");

            migrationBuilder.RenameColumn(
                name: "ORDERID",
                table: "DELIVERY_TASKS",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_DELIVERY_TASKS_ORDERID",
                table: "DELIVERY_TASKS",
                newName: "IX_DELIVERY_TASKS_OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_DELIVERY_TASKS_FOOD_ORDERS_OrderID",
                table: "DELIVERY_TASKS",
                column: "OrderID",
                principalTable: "FOOD_ORDERS",
                principalColumn: "ORDERID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
