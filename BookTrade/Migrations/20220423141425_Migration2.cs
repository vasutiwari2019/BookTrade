using Microsoft.EntityFrameworkCore.Migrations;

namespace BookTrade.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTraded",
                table: "Books",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TradeType",
                table: "Books",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTraded",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TradeType",
                table: "Books");
        }
    }
}
