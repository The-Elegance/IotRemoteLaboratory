using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IotRemoteLab.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class standuarttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uart_Stands_StandId",
                table: "Uart");

            migrationBuilder.DropIndex(
                name: "IX_Uart_StandId",
                table: "Uart");

            migrationBuilder.DropColumn(
                name: "StandId",
                table: "Uart");

            migrationBuilder.CreateTable(
                name: "StandUart",
                columns: table => new
                {
                    StandId = table.Column<long>(type: "bigint", nullable: false),
                    UartId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandUart", x => new { x.StandId, x.UartId });
                    table.ForeignKey(
                        name: "FK_StandUart_Stand",
                        column: x => x.StandId,
                        principalTable: "Stands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandUart_Uart",
                        column: x => x.UartId,
                        principalTable: "Uart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StandUart_UartId",
                table: "StandUart",
                column: "UartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StandUart");

            migrationBuilder.AddColumn<long>(
                name: "StandId",
                table: "Uart",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uart_StandId",
                table: "Uart",
                column: "StandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uart_Stands_StandId",
                table: "Uart",
                column: "StandId",
                principalTable: "Stands",
                principalColumn: "Id");
        }
    }
}
