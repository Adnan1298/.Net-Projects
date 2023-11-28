using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorningSCart.Migrations
{
    /// <inheritdoc />
    public partial class addOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "UserLogins",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "Plant_Id",
                table: "Products",
                newName: "PlantsId");

            migrationBuilder.RenameColumn(
                name: "Plant_Id",
                table: "Orders",
                newName: "UsersId");

            migrationBuilder.AddColumn<int>(
                name: "PlantsId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PlantsId",
                table: "Orders",
                column: "PlantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UsersId",
                table: "Orders",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_PlantsId",
                table: "Orders",
                column: "PlantsId",
                principalTable: "Products",
                principalColumn: "PlantsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserLogins_UsersId",
                table: "Orders",
                column: "UsersId",
                principalTable: "UserLogins",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_PlantsId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserLogins_UsersId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PlantsId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UsersId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PlantsId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserLogins",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "PlantsId",
                table: "Products",
                newName: "Plant_Id");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Orders",
                newName: "Plant_Id");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
