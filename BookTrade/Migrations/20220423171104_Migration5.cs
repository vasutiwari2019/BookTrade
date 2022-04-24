using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookTrade.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeDetails",
                columns: table => new
                {
                    TradeId = table.Column<Guid>(nullable: false),
                    FromUserId = table.Column<Guid>(nullable: false),
                    ToUserId = table.Column<Guid>(nullable: false),
                    RequestedBookId = table.Column<Guid>(nullable: false),
                    TradingBookId = table.Column<Guid>(nullable: false),
                    TradeAccepted = table.Column<bool>(nullable: false),
                    TradeCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeDetails", x => x.TradeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeDetails");
        }
    }
}
