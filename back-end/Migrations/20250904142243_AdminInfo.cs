using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AdminInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PHONENUMBER",
                table: "USERS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "PENALTYNOTE",
                table: "STORE_VIOLATION_PENALTIES",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VIOLATIONPENALTYSTATE",
                table: "STORE_VIOLATION_PENALTIES",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AlterColumn<string>(
                name: "STOREFEATURES",
                table: "STORES",
                type: "NVARCHAR2(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "COMPLAINTSTATE",
                table: "DELIVERY_COMPLAINTS",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AddColumn<string>(
                name: "PROCESSINGREASON",
                table: "DELIVERY_COMPLAINTS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PROCESSINGREMARK",
                table: "DELIVERY_COMPLAINTS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PROCESSINGRESULT",
                table: "DELIVERY_COMPLAINTS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "COMMENTSTATE",
                table: "COMMENTS",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.AddColumn<string>(
                name: "ADMINROLE",
                table: "ADMINISTRATORS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "系统管理员");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PENALTYNOTE",
                table: "STORE_VIOLATION_PENALTIES");

            migrationBuilder.DropColumn(
                name: "VIOLATIONPENALTYSTATE",
                table: "STORE_VIOLATION_PENALTIES");

            migrationBuilder.DropColumn(
                name: "COMPLAINTSTATE",
                table: "DELIVERY_COMPLAINTS");

            migrationBuilder.DropColumn(
                name: "PROCESSINGREASON",
                table: "DELIVERY_COMPLAINTS");

            migrationBuilder.DropColumn(
                name: "PROCESSINGREMARK",
                table: "DELIVERY_COMPLAINTS");

            migrationBuilder.DropColumn(
                name: "PROCESSINGRESULT",
                table: "DELIVERY_COMPLAINTS");

            migrationBuilder.DropColumn(
                name: "COMMENTSTATE",
                table: "COMMENTS");

            migrationBuilder.DropColumn(
                name: "ADMINROLE",
                table: "ADMINISTRATORS");

            migrationBuilder.AlterColumn<string>(
                name: "PHONENUMBER",
                table: "USERS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<string>(
                name: "STOREFEATURES",
                table: "STORES",
                type: "NVARCHAR2(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
