using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleWorkShop.Migrations
{
    public partial class ledgerid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Ledgers_StoreId",
                table: "DamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesDetails_Ledgers_StoreId",
                table: "PurchasesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesReturnDetails_Ledgers_StoreId",
                table: "PurchasesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Ledgers_StoreId",
                table: "SalesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnDetails_Ledgers_StoreId",
                table: "SalesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Ledgers_StoreId",
                table: "Stocks");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Stocks",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_StoreId",
                table: "Stocks",
                newName: "IX_Stocks_LedgerId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "SalesReturnDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesReturnDetails_StoreId",
                table: "SalesReturnDetails",
                newName: "IX_SalesReturnDetails_LedgerId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "SalesDetails",
                newName: "LedgerId");

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
                name: "StoreId",
                table: "DamageDetails",
                newName: "LedgerId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_StoreId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_LedgerId");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseReturnId",
                table: "PurchasesReturnDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasesReturnDetails_PurchaseReturnId",
                table: "PurchasesReturnDetails",
                column: "PurchaseReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Ledgers_LedgerId",
                table: "DamageDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PurchasesReturnDetails_PurchaseReturns_PurchaseReturnId",
                table: "PurchasesReturnDetails",
                column: "PurchaseReturnId",
                principalTable: "PurchaseReturns",
                principalColumn: "PurchaseReturnId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Ledgers_LedgerId",
                table: "SalesDetails",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Ledgers_LedgerId",
                table: "DamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesDetails_Ledgers_LedgerId",
                table: "PurchasesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesReturnDetails_Ledgers_LedgerId",
                table: "PurchasesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasesReturnDetails_PurchaseReturns_PurchaseReturnId",
                table: "PurchasesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetails_Ledgers_LedgerId",
                table: "SalesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturnDetails_Ledgers_LedgerId",
                table: "SalesReturnDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Ledgers_LedgerId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_PurchasesReturnDetails_PurchaseReturnId",
                table: "PurchasesReturnDetails");

            migrationBuilder.DropColumn(
                name: "PurchaseReturnId",
                table: "PurchasesReturnDetails");

            migrationBuilder.RenameColumn(
                name: "LedgerId",
                table: "Stocks",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_LedgerId",
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
                name: "LedgerId",
                table: "SalesDetails",
                newName: "StoreId");

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
                name: "LedgerId",
                table: "DamageDetails",
                newName: "StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_LedgerId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Ledgers_StoreId",
                table: "DamageDetails",
                column: "StoreId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesDetails_Ledgers_StoreId",
                table: "PurchasesDetails",
                column: "StoreId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasesReturnDetails_Ledgers_StoreId",
                table: "PurchasesReturnDetails",
                column: "StoreId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetails_Ledgers_StoreId",
                table: "SalesDetails",
                column: "StoreId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturnDetails_Ledgers_StoreId",
                table: "SalesReturnDetails",
                column: "StoreId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Ledgers_StoreId",
                table: "Stocks",
                column: "StoreId",
                principalTable: "Ledgers",
                principalColumn: "LedgerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
