using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ibreca_data_access.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    Url = table.Column<string>(type: "varchar(512)", nullable: false),
                    UrlAssetId = table.Column<string>(type: "varchar(100)", nullable: false),
                    ShowUntil = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blogentries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    CoverUrl = table.Column<string>(type: "varchar(512)", nullable: true),
                    CoverUrlAssetId = table.Column<string>(type: "varchar(100)", nullable: true),
                    HeaderUrl = table.Column<string>(type: "varchar(512)", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blogentries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "announcements");

            migrationBuilder.DropTable(
                name: "blogentries");
        }
    }
}
