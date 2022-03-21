using Microsoft.EntityFrameworkCore.Migrations;

namespace ibreca_data_access.Migrations
{
    public partial class AssetsIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverUrlAssetId",
                table: "blogentries",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UrlAssetId",
                table: "announcements",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverUrlAssetId",
                table: "blogentries");

            migrationBuilder.DropColumn(
                name: "UrlAssetId",
                table: "announcements");
        }
    }
}
