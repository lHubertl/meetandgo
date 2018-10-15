using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetAndGoApi.Infrastructure.Dal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventDtoId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EventState = table.Column<int>(nullable: false),
                    Transport = table.Column<int>(nullable: false),
                    MaxSeats = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    ExpectedRating = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventDtoId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserDtoId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    OrganizerRating = table.Column<double>(nullable: false),
                    MemberRating = table.Column<double>(nullable: false),
                    CompressedPhotoUrl = table.Column<string>(nullable: true),
                    HighQualityPhoto = table.Column<string>(nullable: true),
                    LanguageCode = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserDtoId);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    PointDtoId = table.Column<Guid>(nullable: false),
                    EventDtoId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Long = table.Column<double>(nullable: false),
                    Lat = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.PointDtoId);
                    table.ForeignKey(
                        name: "FK_Points_Events_EventDtoId",
                        column: x => x.EventDtoId,
                        principalTable: "Events",
                        principalColumn: "EventDtoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentDtoId = table.Column<Guid>(nullable: false),
                    EventDtoId = table.Column<Guid>(nullable: false),
                    UserDtoId = table.Column<Guid>(nullable: false),
                    CommentedIn = table.Column<DateTimeOffset>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentDtoId);
                    table.ForeignKey(
                        name: "FK_Comments_Events_EventDtoId",
                        column: x => x.EventDtoId,
                        principalTable: "Events",
                        principalColumn: "EventDtoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserDtoId",
                        column: x => x.UserDtoId,
                        principalTable: "Users",
                        principalColumn: "UserDtoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    EventUserId = table.Column<Guid>(nullable: false),
                    EventDtoId = table.Column<Guid>(nullable: false),
                    UserDtoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.EventDtoId, x.UserDtoId });
                    table.ForeignKey(
                        name: "FK_EventUser_Events_EventDtoId",
                        column: x => x.EventDtoId,
                        principalTable: "Events",
                        principalColumn: "EventDtoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Users_UserDtoId",
                        column: x => x.UserDtoId,
                        principalTable: "Users",
                        principalColumn: "UserDtoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteDtoId = table.Column<Guid>(nullable: false),
                    UserDtoId = table.Column<Guid>(nullable: false),
                    VoteTargetId = table.Column<Guid>(nullable: false),
                    RatingType = table.Column<int>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteDtoId);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserDtoId",
                        column: x => x.UserDtoId,
                        principalTable: "Users",
                        principalColumn: "UserDtoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventDtoId",
                table: "Comments",
                column: "EventDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserDtoId",
                table: "Comments",
                column: "UserDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_UserDtoId",
                table: "EventUser",
                column: "UserDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_EventDtoId",
                table: "Points",
                column: "EventDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserDtoId",
                table: "Votes",
                column: "UserDtoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
