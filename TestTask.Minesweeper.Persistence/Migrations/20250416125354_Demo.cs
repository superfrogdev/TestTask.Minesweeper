using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Minesweeper.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Demo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gameSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identifier"),
                    MinesCount = table.Column<int>(type: "integer", nullable: false, comment: "Count of mines."),
                    Status = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0, comment: "Status."),
                    FieldSize_Height = table.Column<int>(type: "integer", nullable: false, comment: "Height of game field."),
                    FieldSize_Width = table.Column<int>(type: "integer", nullable: false, comment: "Width of game field.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "turns",
                columns: table => new
                {
                    Number = table.Column<int>(type: "integer", nullable: false, comment: "Number of turn."),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identifier of game session."),
                    CellCoordinates_X = table.Column<short>(type: "smallint", nullable: false, comment: "X-coordinate of target cell."),
                    CellCoordinates_Y = table.Column<short>(type: "smallint", nullable: false, comment: "Y-coordinate of target cell.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turns", x => new { x.Number, x.GameSessionId });
                    table.UniqueConstraint("AK_turns_GameSessionId_Number", x => new { x.GameSessionId, x.Number });
                    table.ForeignKey(
                        name: "FK_turns_gameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "gameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "snapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identifier of snapshot."),
                    GameSessionId = table.Column<Guid>(type: "uuid", nullable: false, comment: "Identifier of game session."),
                    TurnNumber = table.Column<int>(type: "integer", nullable: true, comment: "Number of turn."),
                    Field = table.Column<byte[]>(type: "bytea", nullable: false, comment: "Game field")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_snapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_snapshots_gameSessions_GameSessionId",
                        column: x => x.GameSessionId,
                        principalTable: "gameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_snapshots_turns_GameSessionId_TurnNumber",
                        columns: x => new { x.GameSessionId, x.TurnNumber },
                        principalTable: "turns",
                        principalColumns: new[] { "GameSessionId", "Number" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_snapshots_GameSessionId_TurnNumber",
                table: "snapshots",
                columns: new[] { "GameSessionId", "TurnNumber" },
                unique: true)
                .Annotation("Npgsql:NullsDistinct", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "snapshots");

            migrationBuilder.DropTable(
                name: "turns");

            migrationBuilder.DropTable(
                name: "gameSessions");
        }
    }
}
