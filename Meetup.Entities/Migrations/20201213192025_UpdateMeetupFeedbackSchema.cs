using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meetup.Entities.Migrations
{
    public partial class UpdateMeetupFeedbackSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeedbackIndex",
                table: "MeetupFeedback",
                newName: "SatisfactionIndex");

            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "MeetupFeedback",
                newName: "Comment");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MeetupFeedback",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MeetupFeedback");

            migrationBuilder.RenameColumn(
                name: "SatisfactionIndex",
                table: "MeetupFeedback",
                newName: "FeedbackIndex");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "MeetupFeedback",
                newName: "Feedback");
        }
    }
}
