using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IotRemoteLab.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class University : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademyGroupId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniversityId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcademyGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademyGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversityAcademyGroup",
                columns: table => new
                {
                    AcademyGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    UniversityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityAcademyGroup", x => new { x.AcademyGroupId, x.UniversityId });
                    table.ForeignKey(
                        name: "FK_UniversityAcademyGroup_AcademyGroup",
                        column: x => x.AcademyGroupId,
                        principalTable: "AcademyGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversityAcademyGroup_University",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AcademyGroupId",
                table: "Users",
                column: "AcademyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniversityId",
                table: "Users",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityAcademyGroup_UniversityId",
                table: "UniversityAcademyGroup",
                column: "UniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AcademyGroup_AcademyGroupId",
                table: "Users",
                column: "AcademyGroupId",
                principalTable: "AcademyGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Universities_UniversityId",
                table: "Users",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AcademyGroup_AcademyGroupId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Universities_UniversityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UniversityAcademyGroup");

            migrationBuilder.DropTable(
                name: "AcademyGroup");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_Users_AcademyGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UniversityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AcademyGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupNumber",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
