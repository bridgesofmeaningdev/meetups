using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVK.Meetups.Web.Migrations
{
    public partial class AddJoinTablesToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetupGroupEventAttendee_AspNetUsers_AttendeeId",
                table: "MeetupGroupEventAttendee");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetupGroupEventAttendee_MeetupGroupEvents_EventId",
                table: "MeetupGroupEventAttendee");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetupGroupMember_AspNetUsers_MemberId",
                table: "MeetupGroupMember");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetupGroupMember_MeetupGroups_GroupId",
                table: "MeetupGroupMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetupGroupMember",
                table: "MeetupGroupMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetupGroupEventAttendee",
                table: "MeetupGroupEventAttendee");

            migrationBuilder.RenameTable(
                name: "MeetupGroupMember",
                newName: "GroupMembers");

            migrationBuilder.RenameTable(
                name: "MeetupGroupEventAttendee",
                newName: "EventAttendees");

            migrationBuilder.RenameIndex(
                name: "IX_MeetupGroupMember_MemberId",
                table: "GroupMembers",
                newName: "IX_GroupMembers_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetupGroupEventAttendee_AttendeeId",
                table: "EventAttendees",
                newName: "IX_EventAttendees_AttendeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers",
                columns: new[] { "GroupId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventAttendees",
                table: "EventAttendees",
                columns: new[] { "EventId", "AttendeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_AspNetUsers_AttendeeId",
                table: "EventAttendees",
                column: "AttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventAttendees_MeetupGroupEvents_EventId",
                table: "EventAttendees",
                column: "EventId",
                principalTable: "MeetupGroupEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_AspNetUsers_MemberId",
                table: "GroupMembers",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMembers_MeetupGroups_GroupId",
                table: "GroupMembers",
                column: "GroupId",
                principalTable: "MeetupGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_AspNetUsers_AttendeeId",
                table: "EventAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_EventAttendees_MeetupGroupEvents_EventId",
                table: "EventAttendees");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_AspNetUsers_MemberId",
                table: "GroupMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMembers_MeetupGroups_GroupId",
                table: "GroupMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupMembers",
                table: "GroupMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventAttendees",
                table: "EventAttendees");

            migrationBuilder.RenameTable(
                name: "GroupMembers",
                newName: "MeetupGroupMember");

            migrationBuilder.RenameTable(
                name: "EventAttendees",
                newName: "MeetupGroupEventAttendee");

            migrationBuilder.RenameIndex(
                name: "IX_GroupMembers_MemberId",
                table: "MeetupGroupMember",
                newName: "IX_MeetupGroupMember_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_EventAttendees_AttendeeId",
                table: "MeetupGroupEventAttendee",
                newName: "IX_MeetupGroupEventAttendee_AttendeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetupGroupMember",
                table: "MeetupGroupMember",
                columns: new[] { "GroupId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetupGroupEventAttendee",
                table: "MeetupGroupEventAttendee",
                columns: new[] { "EventId", "AttendeeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupGroupEventAttendee_AspNetUsers_AttendeeId",
                table: "MeetupGroupEventAttendee",
                column: "AttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupGroupEventAttendee_MeetupGroupEvents_EventId",
                table: "MeetupGroupEventAttendee",
                column: "EventId",
                principalTable: "MeetupGroupEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupGroupMember_AspNetUsers_MemberId",
                table: "MeetupGroupMember",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetupGroupMember_MeetupGroups_GroupId",
                table: "MeetupGroupMember",
                column: "GroupId",
                principalTable: "MeetupGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
