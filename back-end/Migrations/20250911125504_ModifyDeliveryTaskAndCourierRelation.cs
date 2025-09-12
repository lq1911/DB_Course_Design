using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDeliveryTaskAndCourierRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DELIVERY_TASKS_COURIERS_COURIERID",
                table: "DELIVERY_TASKS");

            migrationBuilder.AlterColumn<int>(
                name: "COURIERID",
                table: "DELIVERY_TASKS",
                type: "NUMBER(10)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_DELIVERY_TASKS_COURIERS_COURIERID",
                table: "DELIVERY_TASKS",
                column: "COURIERID",
                principalTable: "COURIERS",
                principalColumn: "USERID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DELIVERY_TASKS_COURIERS_COURIERID",
                table: "DELIVERY_TASKS");

            migrationBuilder.AlterColumn<int>(
                name: "COURIERID",
                table: "DELIVERY_TASKS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DELIVERY_TASKS_COURIERS_COURIERID",
                table: "DELIVERY_TASKS",
                column: "COURIERID",
                principalTable: "COURIERS",
                principalColumn: "USERID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
