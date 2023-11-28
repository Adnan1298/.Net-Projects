using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MorningSCart.Migrations
{
    /// <inheritdoc />
    public partial class addCC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CreditCard",
                table: "Orders",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CreditCard",
                table: "Orders",
                type: "int",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
