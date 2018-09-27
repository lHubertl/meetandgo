using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetAndGoApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventModelId = table.Column<Guid>(nullable: false),
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
                    table.PrimaryKey("PK_Events", x => x.EventModelId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberModelId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    OrganizerRating = table.Column<double>(nullable: false),
                    MemberRating = table.Column<double>(nullable: false),
                    CompressedPhotoUrl = table.Column<string>(nullable: true),
                    HighQualityPhoto = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    EventModelId = table.Column<Guid>(nullable: true),
                    UserModelId = table.Column<Guid>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    LanguageCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberModelId);
                    table.ForeignKey(
                        name: "FK_Members_Events_EventModelId",
                        column: x => x.EventModelId,
                        principalTable: "Events",
                        principalColumn: "EventModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    PointModelId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Long = table.Column<double>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    EventModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.PointModelId);
                    table.ForeignKey(
                        name: "FK_Points_Events_EventModelId",
                        column: x => x.EventModelId,
                        principalTable: "Events",
                        principalColumn: "EventModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentModelId = table.Column<Guid>(nullable: false),
                    AuthorMemberModelId = table.Column<Guid>(nullable: true),
                    EventModelId = table.Column<Guid>(nullable: true),
                    CommentedIn = table.Column<DateTimeOffset>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentModelId);
                    table.ForeignKey(
                        name: "FK_Comments_Members_AuthorMemberModelId",
                        column: x => x.AuthorMemberModelId,
                        principalTable: "Members",
                        principalColumn: "MemberModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Events_EventModelId",
                        column: x => x.EventModelId,
                        principalTable: "Events",
                        principalColumn: "EventModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteModelId = table.Column<Guid>(nullable: false),
                    VoterMemberModelId = table.Column<Guid>(nullable: true),
                    TargetMemberModelId = table.Column<Guid>(nullable: true),
                    RatingType = table.Column<int>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    UserModelMemberModelId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteModelId);
                    table.ForeignKey(
                        name: "FK_Votes_Members_TargetMemberModelId",
                        column: x => x.TargetMemberModelId,
                        principalTable: "Members",
                        principalColumn: "MemberModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Members_UserModelMemberModelId",
                        column: x => x.UserModelMemberModelId,
                        principalTable: "Members",
                        principalColumn: "MemberModelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Members_VoterMemberModelId",
                        column: x => x.VoterMemberModelId,
                        principalTable: "Members",
                        principalColumn: "MemberModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorMemberModelId",
                table: "Comments",
                column: "AuthorMemberModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventModelId",
                table: "Comments",
                column: "EventModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_EventModelId",
                table: "Members",
                column: "EventModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_EventModelId",
                table: "Points",
                column: "EventModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_TargetMemberModelId",
                table: "Votes",
                column: "TargetMemberModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserModelMemberModelId",
                table: "Votes",
                column: "UserModelMemberModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterMemberModelId",
                table: "Votes",
                column: "VoterMemberModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
