using Microsoft.EntityFrameworkCore.Migrations;

namespace Fritz.ResourceManagement.Web.Migrations
{
    public partial class ManyPersonTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonTypes_Persons_PersonId",
                table: "PersonTypes");

            migrationBuilder.DropIndex(
                name: "IX_PersonTypes_PersonId",
                table: "PersonTypes");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "PersonTypes");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPersonType");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "PersonTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonTypes_PersonId",
                table: "PersonTypes",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonTypes_Persons_PersonId",
                table: "PersonTypes",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
