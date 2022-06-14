using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    public partial class addFormatType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rawText",
                table: "Markdown",
                newName: "RawText");

            migrationBuilder.AddColumn<string>(
                name: "FormatType",
                table: "Markdown",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormatType",
                table: "Markdown");

            migrationBuilder.RenameColumn(
                name: "RawText",
                table: "Markdown",
                newName: "rawText");
        }
    }
}
