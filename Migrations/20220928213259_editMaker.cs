using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    public partial class editMaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Maker");

            migrationBuilder.DropColumn(
                name: "Founded",
                table: "Maker");

            migrationBuilder.DropColumn(
                name: "Related",
                table: "Maker");

            migrationBuilder.DropColumn(
                name: "WikiLink",
                table: "Maker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Maker",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Founded",
                table: "Maker",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Related",
                table: "Maker",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WikiLink",
                table: "Maker",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }
    }
}
