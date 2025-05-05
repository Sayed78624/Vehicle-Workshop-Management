using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleWorkShop.Migrations
{
    public partial class vehic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Ledgers_LedgerId",
                table: "DamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Customers_CustomerId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesDetails_Ledgers_LedgerId",
                table: "PurchasesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesReturnDetails_Ledgers_LedgerId",
                table: "PurchasesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Ledgers_LedgerId",
                table: "SalesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_WorkShops_WorkshopId",
                table: "SalesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnDetails_Ledgers_LedgerId",
                table: "SalesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Ledgers_LedgerId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockTypes_StockTypeId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_LedgerId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "LedgerId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "StockTypeId",
                table: "Stocks",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_StockTypeId",
                table: "Stocks",
                newName: "IX_Stocks_StoreId");

            migrationBuilder.RenameColumn(
                name: "LedgerId",
                table: "SalesReturnDetails",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnDetails_LedgerId",
                table: "SalesReturnDetails",
                newName: "IX_SalesReturnDetails_StoreId");

            migrationBuilder.RenameColumn(
                name: "WorkshopId",
                table: "SalesDetails",
                newName: "WorkShopId");

            migrationBuilder.RenameColumn(
                name: "LedgerId",
                table: "SalesDetails",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_WorkshopId",
                table: "SalesDetails",
                newName: "IX_SalesDetails_WorkShopId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_LedgerId",
                table: "SalesDetails",
                newName: "IX_SalesDetails_StoreId");

            migrationBuilder.RenameColumn(
                name: "LedgerId",
                table: "PurchasesReturnDetails",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesReturnDetails_LedgerId",
                table: "PurchasesReturnDetails",
                newName: "IX_PurchasesReturnDetails_StoreId");

            migrationBuilder.RenameColumn(
                name: "LedgerId",
                table: "PurchasesDetails",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesDetails_LedgerId",
                table: "PurchasesDetails",
                newName: "IX_PurchasesDetails_StoreId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Ledgers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ledgers_CustomerId",
                table: "Ledgers",
                newName: "IX_Ledgers_UserId");

            migrationBuilder.RenameColumn(
                name: "LedgerId",
                table: "DamageDetails",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_LedgerId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_StoreId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_StoreId",
                table: "Ledgers",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Stores_StoreId",
                table: "DamageDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Stores_StoreId",
                table: "Ledgers",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Users_UserId",
                table: "Ledgers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesDetails_Stores_StoreId",
                table: "PurchasesDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesReturnDetails_Stores_StoreId",
                table: "PurchasesReturnDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Stores_StoreId",
                table: "SalesDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_WorkShops_WorkShopId",
                table: "SalesDetails",
                column: "WorkShopId",
                principalTable: "WorkShops",
                principalColumn: "WorkShopId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnDetails_Stores_StoreId",
                table: "SalesReturnDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Stores_StoreId",
                table: "Stocks",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Stores_StoreId",
                table: "DamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Stores_StoreId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Users_UserId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesDetails_Stores_StoreId",
                table: "PurchasesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesReturnDetails_Stores_StoreId",
                table: "PurchasesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Stores_StoreId",
                table: "SalesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_WorkShops_WorkShopId",
                table: "SalesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnDetails_Stores_StoreId",
                table: "SalesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Stores_StoreId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_StoreId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Ledgers");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Stocks",
                newName: "StockTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_StoreId",
                table: "Stocks",
                newName: "IX_Stocks_StockTypeId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "SalesReturnDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnDetails_StoreId",
                table: "SalesReturnDetails",
                newName: "IX_SalesReturnDetails_LedgerId");

            migrationBuilder.RenameColumn(
                name: "WorkShopId",
                table: "SalesDetails",
                newName: "WorkshopId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "SalesDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_WorkShopId",
                table: "SalesDetails",
                newName: "IX_SalesDetails_WorkshopId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesDetails_StoreId",
                table: "SalesDetails",
                newName: "IX_SalesDetails_LedgerId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "PurchasesReturnDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesReturnDetails_StoreId",
                table: "PurchasesReturnDetails",
                newName: "IX_PurchasesReturnDetails_LedgerId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "PurchasesDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasesDetails_StoreId",
                table: "PurchasesDetails",
                newName: "IX_PurchasesDetails_LedgerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ledgers",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ledgers_UserId",
                table: "Ledgers",
                newName: "IX_Ledgers_CustomerId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "DamageDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_StoreId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_LedgerId");

            migrationBuilder.AddColumn<int>(
                name: "LedgerId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductName",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_LedgerId",
                table: "Stocks",
                column: "LedgerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Ledgers_LedgerId",
                table: "DamageDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Customers_CustomerId",
                table: "Ledgers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesDetails_Ledgers_LedgerId",
                table: "PurchasesDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesReturnDetails_Ledgers_LedgerId",
                table: "PurchasesReturnDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Ledgers_LedgerId",
                table: "SalesDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_WorkShops_WorkshopId",
                table: "SalesDetails",
                column: "WorkshopId",
                principalTable: "WorkShops",
                principalColumn: "WorkShopId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnDetails_Ledgers_LedgerId",
                table: "SalesReturnDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Ledgers_LedgerId",
                table: "Stocks",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockTypes_StockTypeId",
                table: "Stocks",
                column: "StockTypeId",
                principalTable: "StockTypes",
                principalColumn: "StockTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
