using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRedundantAdminFKFromUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_ADMINISTRATORS_AdministratorUserID",
                table: "USERS");

            migrationBuilder.DropIndex(
                name: "IX_USERS_AdministratorUserID",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "AdministratorUserID",
                table: "USERS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministratorUserID",
                table: "USERS",
                type: "NUMBER(10)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_AdministratorUserID",
                table: "USERS",
                column: "AdministratorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_ADMINISTRATORS_AdministratorUserID",
                table: "USERS",
                column: "AdministratorUserID",
                principalTable: "ADMINISTRATORS",
                principalColumn: "USERID");
        }
    }
}
