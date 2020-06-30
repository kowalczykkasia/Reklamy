using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class AddedColumnSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "salt",
                table: "Client",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salt",
                table: "Client");
        }
    }
}
