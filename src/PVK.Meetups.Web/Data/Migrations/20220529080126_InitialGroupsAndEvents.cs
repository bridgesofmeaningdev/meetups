using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PVK.Meetups.Web.Migrations
{
    public partial class InitialGroupsAndEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationDescription",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeetupGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    LocationDescription = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeetupGroupEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    LocationDescription = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OwningMeetupGroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupGroupEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetupGroupEvents_MeetupGroups_OwningMeetupGroupId",
                        column: x => x.OwningMeetupGroupId,
                        principalTable: "MeetupGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetupGroupMember",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    MemberId = table.Column<string>(type: "text", nullable: false),
                    IsGroupOrganizer = table.Column<bool>(type: "boolean", nullable: false),
                    IsPrimaryGroupOrganizer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupGroupMember", x => new { x.GroupId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MeetupGroupMember_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetupGroupMember_MeetupGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "MeetupGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetupGroupEventAttendee",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    AttendeeId = table.Column<string>(type: "text", nullable: false),
                    IsPrimaryEventHost = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetupGroupEventAttendee", x => new { x.EventId, x.AttendeeId });
                    table.ForeignKey(
                        name: "FK_MeetupGroupEventAttendee_AspNetUsers_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetupGroupEventAttendee_MeetupGroupEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "MeetupGroupEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetupGroupEventAttendee_AttendeeId",
                table: "MeetupGroupEventAttendee",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetupGroupEvents_OwningMeetupGroupId",
                table: "MeetupGroupEvents",
                column: "OwningMeetupGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetupGroupMember_MemberId",
                table: "MeetupGroupMember",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetupGroupEventAttendee");

            migrationBuilder.DropTable(
                name: "MeetupGroupMember");

            migrationBuilder.DropTable(
                name: "MeetupGroupEvents");

            migrationBuilder.DropTable(
                name: "MeetupGroups");

            migrationBuilder.DropColumn(
                name: "LocationDescription",
                table: "AspNetUsers");
        }
    }
}
