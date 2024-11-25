using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IotRemoteLab.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleStandTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stands_Schedule_ScheduleBaseId",
                table: "Stands");

            migrationBuilder.DropIndex(
                name: "IX_Stands_ScheduleBaseId",
                table: "Stands");

            migrationBuilder.DropColumn(
                name: "ScheduleBaseId",
                table: "Stands");

            migrationBuilder.CreateTable(
                name: "ScheduleStand",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    StandId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleStand", x => new { x.ScheduleId, x.StandId });
                    table.ForeignKey(
                        name: "FK_ScheduleStand_Schedule",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleStand_Stand",
                        column: x => x.StandId,
                        principalTable: "Stands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleStand_StandId",
                table: "ScheduleStand",
                column: "StandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleStand");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleBaseId",
                table: "Stands",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stands_ScheduleBaseId",
                table: "Stands",
                column: "ScheduleBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stands_Schedule_ScheduleBaseId",
                table: "Stands",
                column: "ScheduleBaseId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }
    }
}
