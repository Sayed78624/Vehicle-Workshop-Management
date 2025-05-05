using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleWorkShop.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Users_UserId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_UserId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Ledgers");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductName",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Ledgers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_CustomerId",
                table: "Ledgers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Customers_CustomerId",
                table: "Ledgers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Customers_CustomerId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_CustomerId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Ledgers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_UserId",
                table: "Ledgers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Users_UserId",
                table: "Ledgers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
