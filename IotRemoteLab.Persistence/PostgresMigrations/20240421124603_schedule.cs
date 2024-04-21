using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IotRemoteLab.Persistence.PostgresMigrations
{
    /// <inheritdoc />
    public partial class schedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benchboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchboard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "McuFramework",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Pattern = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_McuFramework", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BenchboardPort",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    RaspberryPiPort = table.Column<long>(type: "bigint", nullable: false),
                    McuPort = table.Column<string>(type: "text", nullable: false),
                    BenchboardId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchboardPort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenchboardPort_Benchboard_BenchboardId",
                        column: x => x.BenchboardId,
                        principalTable: "Benchboard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mcu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FrameworkId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssemblyScriptFile = table.Column<string>(type: "text", nullable: false),
                    DeployScriptFile = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mcu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mcu_McuFramework_FrameworkId",
                        column: x => x.FrameworkId,
                        principalTable: "McuFramework",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    McuId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    HasBenchboard = table.Column<bool>(type: "boolean", nullable: false),
                    BenchboardId = table.Column<Guid>(type: "uuid", nullable: false),
                    HasLighting = table.Column<bool>(type: "boolean", nullable: false),
                    LightingBrightnessLevel = table.Column<long>(type: "bigint", nullable: false),
                    LightingRaspberryPiPort = table.Column<long>(type: "bigint", nullable: false),
                    HasSerialPort = table.Column<bool>(type: "boolean", nullable: false),
                    SerialPortSpeed = table.Column<long>(type: "bigint", nullable: false),
                    HasWebcam = table.Column<bool>(type: "boolean", nullable: false),
                    WebcamUrl = table.Column<string>(type: "text", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stand", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stand_Benchboard_BenchboardId",
                        column: x => x.BenchboardId,
                        principalTable: "Benchboard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stand_Mcu_McuId",
                        column: x => x.McuId,
                        principalTable: "Mcu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stand_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenchboardPort_BenchboardId",
                table: "BenchboardPort",
                column: "BenchboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Mcu_FrameworkId",
                table: "Mcu",
                column: "FrameworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_TeamId",
                table: "Schedule",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Stand_BenchboardId",
                table: "Stand",
                column: "BenchboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Stand_McuId",
                table: "Stand",
                column: "McuId");

            migrationBuilder.CreateIndex(
                name: "IX_Stand_ScheduleId",
                table: "Stand",
                column: "ScheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenchboardPort");

            migrationBuilder.DropTable(
                name: "Stand");

            migrationBuilder.DropTable(
                name: "Benchboard");

            migrationBuilder.DropTable(
                name: "Mcu");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "McuFramework");
        }
    }
}
