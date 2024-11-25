using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IotRemoteLab.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleHolder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Teams_TeamId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Stands_Schedule_ScheduleId",
                table: "Stands");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "Stands",
                newName: "ScheduleBaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Stands_ScheduleId",
                table: "Stands",
                newName: "IX_Stands_ScheduleBaseId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Schedule",
                newName: "HolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_TeamId",
                table: "Schedule",
                newName: "IX_Schedule_HolderId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Schedule",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Schedule",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Teams_HolderId",
                table: "Schedule",
                column: "HolderId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Users_HolderId",
                table: "Schedule",
                column: "HolderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stands_Schedule_ScheduleBaseId",
                table: "Stands",
                column: "ScheduleBaseId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Teams_HolderId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Users_HolderId",
                table: "Schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_Stands_Schedule_ScheduleBaseId",
                table: "Stands");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Schedule");

            migrationBuilder.RenameColumn(
                name: "ScheduleBaseId",
                table: "Stands",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Stands_ScheduleBaseId",
                table: "Stands",
                newName: "IX_Stands_ScheduleId");

            migrationBuilder.RenameColumn(
                name: "HolderId",
                table: "Schedule",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_HolderId",
                table: "Schedule",
                newName: "IX_Schedule_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Teams_TeamId",
                table: "Schedule",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stands_Schedule_ScheduleId",
                table: "Stands",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }
    }
}
