using Microsoft.EntityFrameworkCore.Migrations;

namespace JustTradeIt.Software.API.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trades_Items_ItemId",
                table: "Trades");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Trades_TradeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TradeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Trades_ItemId",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "TradeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Trades");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TradeId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Trades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TradeId",
                table: "Users",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_ItemId",
                table: "Trades",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trades_Items_ItemId",
                table: "Trades",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Trades_TradeId",
                table: "Users",
                column: "TradeId",
                principalTable: "Trades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
