using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    public partial class removeStageObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stage_IdStage",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Stage");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdStage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdStage",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "IdStage",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    StageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", nullable: false),
                    NbSessionsRequis = table.Column<int>(type: "int", nullable: false),
                    PermisRequis = table.Column<int>(type: "int", nullable: false),
                    StageRequis = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.StageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdStage",
                table: "AspNetUsers",
                column: "IdStage");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stage_IdStage",
                table: "AspNetUsers",
                column: "IdStage",
                principalTable: "Stage",
                principalColumn: "StageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
