using Microsoft.EntityFrameworkCore.Migrations;

namespace BookTrade.Migrations
{
    public partial class Migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TradeDelivered",
                table: "TradeDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TradeDelivered",
                table: "TradeDetails");
        }
    }
}
