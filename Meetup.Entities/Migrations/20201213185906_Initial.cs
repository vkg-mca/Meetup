using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meetup.Entities.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Topic = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    ParticipantsCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetupDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    Topic = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    ParticipantsCount = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meetup");

            migrationBuilder.DropTable(
                name: "MeetupDetail");
        }
    }
}
