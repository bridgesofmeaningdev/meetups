using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVK.Meetups.Web.Migrations
{
    public partial class ChangeDateTimeColumnsToUtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "MeetupGroupEvents",
                newName: "StartDateTimeUtc");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "MeetupGroupEvents",
                newName: "EndDateTimeUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTimeUtc",
                table: "MeetupGroupEvents",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndDateTimeUtc",
                table: "MeetupGroupEvents",
                newName: "EndDateTime");
        }
    }
}
