using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApplicationBackend.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaderBoards_Quizes_QuizId",
                table: "LeaderBoards");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_leaderboard",
                table: "LeaderBoards",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_leaderboard",
                table: "LeaderBoards");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaderBoards_Quizes_QuizId",
                table: "LeaderBoards",
                column: "QuizId",
                principalTable: "Quizes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
