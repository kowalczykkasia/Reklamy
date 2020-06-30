using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class AddedAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    IdBuilding = table.Column<int>(nullable: false),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Building_PK", x => x.IdBuilding);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<string>(maxLength: 100, nullable: false),
                    Login = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Client_PK", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    IdCampaign = table.Column<int>(nullable: false),
                    IdClient = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PricePerSquareMeter = table.Column<double>(nullable: false),
                    FromIdBuilding = table.Column<int>(nullable: false),
                    ToIdBuilding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Campaign_PK", x => x.IdCampaign);
                    table.ForeignKey(
                        name: "BuildingFrom_Campaign_FK",
                        column: x => x.FromIdBuilding,
                        principalTable: "Building",
                        principalColumn: "IdBuilding",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Client_Campaign_FK",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "BuildingTo_Campaign_FK",
                        column: x => x.ToIdBuilding,
                        principalTable: "Building",
                        principalColumn: "IdBuilding",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    IdAdvertisement = table.Column<int>(nullable: false),
                    Name = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    IdCampaign = table.Column<int>(nullable: false),
                    Area = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Banner_PK", x => x.IdAdvertisement);
                    table.ForeignKey(
                        name: "Banner_Campaign_FK",
                        column: x => x.IdCampaign,
                        principalTable: "Campaign",
                        principalColumn: "IdCampaign",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_IdCampaign",
                table: "Banner",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_FromIdBuilding",
                table: "Campaign",
                column: "FromIdBuilding");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_IdClient",
                table: "Campaign",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_ToIdBuilding",
                table: "Campaign",
                column: "ToIdBuilding");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
