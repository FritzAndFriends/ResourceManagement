using Microsoft.EntityFrameworkCore.Migrations;

namespace Fritz.ResourceManagement.Web.Migrations.ApplicationDb
{
    public partial class AddScheduleIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "AspNetUsers");
        }
    }
}
