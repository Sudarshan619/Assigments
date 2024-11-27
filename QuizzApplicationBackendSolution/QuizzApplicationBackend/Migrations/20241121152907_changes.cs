using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApplicationBackend.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScoreCards_LeaderBoards_LeaderBoardId",
                table: "ScoreCards");

            migrationBuilder.DropIndex(
                name: "IX_ScoreCards_LeaderBoardId",
                table: "ScoreCards");

            migrationBuilder.DropColumn(
                name: "LeaderBoardId",
                table: "ScoreCards");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeaderBoardId",
                table: "ScoreCards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoreCards_LeaderBoardId",
                table: "ScoreCards",
                column: "LeaderBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreCards_LeaderBoards_LeaderBoardId",
                table: "ScoreCards",
                column: "LeaderBoardId",
                principalTable: "LeaderBoards",
                principalColumn: "LeaderBoardId");
        }
    }
}
