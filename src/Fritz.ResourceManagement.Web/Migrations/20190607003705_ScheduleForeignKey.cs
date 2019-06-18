using Microsoft.EntityFrameworkCore.Migrations;

namespace Fritz.ResourceManagement.Web.Migrations
{
    public partial class ScheduleForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Persons_ScheduleId",
                table: "Persons",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Schedules_ScheduleId",
                table: "Persons",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Schedules_ScheduleId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ScheduleId",
                table: "Persons");
        }
    }
}
