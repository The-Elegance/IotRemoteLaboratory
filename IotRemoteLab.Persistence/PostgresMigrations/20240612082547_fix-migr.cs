using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IotRemoteLab.Persistence.PostgresMigrations
{
    /// <inheritdoc />
    public partial class fixmigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stand_Benchboard_BenchboardId",
                table: "Stand");

            migrationBuilder.DropForeignKey(
                name: "FK_Stand_Mcu_McuId",
                table: "Stand");

            migrationBuilder.DropForeignKey(
                name: "FK_Stand_Schedule_ScheduleId",
                table: "Stand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stand",
                table: "Stand");

            migrationBuilder.RenameTable(
                name: "Stand",
                newName: "Stands");

            migrationBuilder.RenameIndex(
                name: "IX_Stand_ScheduleId",
                table: "Stands",
                newName: "IX_Stands_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Stand_McuId",
                table: "Stands",
                newName: "IX_Stands_McuId");

            migrationBuilder.RenameIndex(
                name: "IX_Stand_BenchboardId",
                table: "Stands",
                newName: "IX_Stands_BenchboardId");

            migrationBuilder.AddColumn<string>(
                name: "CodeFileExtension",
                table: "McuFramework",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodeFileName",
                table: "McuFramework",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "BenchboardId",
                table: "Stands",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "CodeEditorId",
                table: "Stands",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stands",
                table: "Stands",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Uart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<byte>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    StandId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uart_Stands_StandId",
                        column: x => x.StandId,
                        principalTable: "Stands",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uart_StandId",
                table: "Uart",
                column: "StandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stands_Benchboard_BenchboardId",
                table: "Stands",
                column: "BenchboardId",
                principalTable: "Benchboard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stands_Mcu_McuId",
                table: "Stands",
                column: "McuId",
                principalTable: "Mcu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stands_Schedule_ScheduleId",
                table: "Stands",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stands_Benchboard_BenchboardId",
                table: "Stands");

            migrationBuilder.DropForeignKey(
                name: "FK_Stands_Mcu_McuId",
                table: "Stands");

            migrationBuilder.DropForeignKey(
                name: "FK_Stands_Schedule_ScheduleId",
                table: "Stands");

            migrationBuilder.DropTable(
                name: "Uart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stands",
                table: "Stands");

            migrationBuilder.DropColumn(
                name: "CodeFileExtension",
                table: "McuFramework");

            migrationBuilder.DropColumn(
                name: "CodeFileName",
                table: "McuFramework");

            migrationBuilder.DropColumn(
                name: "CodeEditorId",
                table: "Stands");

            migrationBuilder.RenameTable(
                name: "Stands",
                newName: "Stand");

            migrationBuilder.RenameIndex(
                name: "IX_Stands_ScheduleId",
                table: "Stand",
                newName: "IX_Stand_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Stands_McuId",
                table: "Stand",
                newName: "IX_Stand_McuId");

            migrationBuilder.RenameIndex(
                name: "IX_Stands_BenchboardId",
                table: "Stand",
                newName: "IX_Stand_BenchboardId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BenchboardId",
                table: "Stand",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stand",
                table: "Stand",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stand_Benchboard_BenchboardId",
                table: "Stand",
                column: "BenchboardId",
                principalTable: "Benchboard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stand_Mcu_McuId",
                table: "Stand",
                column: "McuId",
                principalTable: "Mcu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stand_Schedule_ScheduleId",
                table: "Stand",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "Id");
        }
    }
}
