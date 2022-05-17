using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eventify.DAL.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_LegalPerson_LegalPersonId",
                table: "Attendee");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_PhysicalPerson_PhysicalPersonId",
                table: "Attendee");

            migrationBuilder.DropTable(
                name: "LegalPerson");

            migrationBuilder.DropTable(
                name: "PhysicalPerson");

            migrationBuilder.RenameColumn(
                name: "PhysicalPersonId",
                table: "Attendee",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "LegalPersonId",
                table: "Attendee",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendee_PhysicalPersonId",
                table: "Attendee",
                newName: "IX_Attendee_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendee_LegalPersonId",
                table: "Attendee",
                newName: "IX_Attendee_CompanyId");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PersonalCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_Company_CompanyId",
                table: "Attendee",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_Person_PersonId",
                table: "Attendee",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_Company_CompanyId",
                table: "Attendee");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendee_Person_PersonId",
                table: "Attendee");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Attendee",
                newName: "PhysicalPersonId");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Attendee",
                newName: "LegalPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendee_PersonId",
                table: "Attendee",
                newName: "IX_Attendee_PhysicalPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendee_CompanyId",
                table: "Attendee",
                newName: "IX_Attendee_LegalPersonId");

            migrationBuilder.CreateTable(
                name: "LegalPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalPerson", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PersonalCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPerson", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_LegalPerson_LegalPersonId",
                table: "Attendee",
                column: "LegalPersonId",
                principalTable: "LegalPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendee_PhysicalPerson_PhysicalPersonId",
                table: "Attendee",
                column: "PhysicalPersonId",
                principalTable: "PhysicalPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
