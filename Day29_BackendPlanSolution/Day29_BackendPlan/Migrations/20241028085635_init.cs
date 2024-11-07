using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Day29_BackendPlan.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HashKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "PolicyHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyHolders", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_PolicyHolder_user",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimReports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyHolderId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeathCertificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyCertificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancelledCheck = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimReports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_ClaimReport_PolicyHolder",
                        column: x => x.PolicyHolderId,
                        principalTable: "PolicyHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyItems",
                columns: table => new
                {
                    SNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    PolicyHolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyItems", x => x.SNo);
                    table.ForeignKey(
                        name: "FK_PolicyItems_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyItems_PolicyHolders_PolicyHolderId",
                        column: x => x.PolicyHolderId,
                        principalTable: "PolicyHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimReports_PolicyHolderId",
                table: "ClaimReports",
                column: "PolicyHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyHolders_Username",
                table: "PolicyHolders",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PolicyItems_PolicyHolderId",
                table: "PolicyItems",
                column: "PolicyHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyItems_PolicyId",
                table: "PolicyItems",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimReports");

            migrationBuilder.DropTable(
                name: "PolicyItems");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "PolicyHolders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
