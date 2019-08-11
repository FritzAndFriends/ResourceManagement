using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fritz.ResourceManagement.Web.Migrations
{
    public partial class NewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GivenName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CronPattern = table.Column<string>(nullable: true),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    MinStartDateTime = table.Column<DateTime>(nullable: false),
                    MaxEndDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringSchedule_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ScheduleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleException", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleException_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScheduleId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleItem_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPersonType",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    PersonTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPersonType", x => new { x.PersonId, x.PersonTypeId });
                    table.ForeignKey(
                        name: "FK_PersonPersonType_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonPersonType_PersonTypes_PersonTypeId",
                        column: x => x.PersonTypeId,
                        principalTable: "PersonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPersonType_PersonTypeId",
                table: "PersonPersonType",
                column: "PersonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ScheduleId",
                table: "Persons",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringSchedule_ScheduleId",
                table: "RecurringSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleException_ScheduleId",
                table: "ScheduleException",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleItem_ScheduleId",
                table: "ScheduleItem",
                column: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPersonType");

            migrationBuilder.DropTable(
                name: "RecurringSchedule");

            migrationBuilder.DropTable(
                name: "ScheduleException");

            migrationBuilder.DropTable(
                name: "ScheduleItem");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
