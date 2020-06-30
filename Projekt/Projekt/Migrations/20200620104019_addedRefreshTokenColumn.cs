using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class addedRefreshTokenColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Client",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Client");
        }
    }
}
