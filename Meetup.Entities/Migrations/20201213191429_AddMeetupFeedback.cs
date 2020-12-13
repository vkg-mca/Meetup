using Microsoft.EntityFrameworkCore.Migrations;

namespace Meetup.Entities.Migrations
{
    public partial class AddMeetupFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetupFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    FeedbackIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupFeedback", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetupFeedback");
        }
    }
}
