using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApplicationBackend.Migrations
{
    public partial class addedsubmissiontime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedTime",
                table: "ScoreCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedTime",
                table: "ScoreCards");
        }
    }
}
