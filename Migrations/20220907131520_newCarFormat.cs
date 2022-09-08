using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    public partial class newCarFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aspiration",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "EngineDisplacement",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "GearBox",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "PowerBHP",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "PowerKW",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "TorqueLBFT",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "TorqueNM",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "WeightLBS",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "Aspiration",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "EngineConfiguration",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "EngineDisplacement",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "GearBox",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "PowerBHP",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "PowerKW",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "TorqueLBFT",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "TorqueNM",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "OriginalCar");

            migrationBuilder.DropColumn(
                name: "WeightLBS",
                table: "OriginalCar");

            migrationBuilder.RenameColumn(
                name: "NbCylindre",
                table: "Voitures",
                newName: "PowerHp");

            migrationBuilder.RenameColumn(
                name: "EnginePosition",
                table: "Voitures",
                newName: "DriveTrain");

            migrationBuilder.RenameColumn(
                name: "NbCylindre",
                table: "OriginalCar",
                newName: "PowerHp");

            migrationBuilder.RenameColumn(
                name: "EnginePosition",
                table: "OriginalCar",
                newName: "DriveTrain");

            migrationBuilder.AddColumn<bool>(
                name: "OnRoad",
                table: "Voitures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "RequiredDLC",
                table: "OriginalCar",
                type: "bit",
                unicode: false,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "PictureLink",
                table: "OriginalCar",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnRoad",
                table: "OriginalCar",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnRoad",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "OnRoad",
                table: "OriginalCar");

            migrationBuilder.RenameColumn(
                name: "PowerHp",
                table: "Voitures",
                newName: "NbCylindre");

            migrationBuilder.RenameColumn(
                name: "DriveTrain",
                table: "Voitures",
                newName: "EnginePosition");

            migrationBuilder.RenameColumn(
                name: "PowerHp",
                table: "OriginalCar",
                newName: "NbCylindre");

            migrationBuilder.RenameColumn(
                name: "DriveTrain",
                table: "OriginalCar",
                newName: "EnginePosition");

            migrationBuilder.AddColumn<int>(
                name: "Aspiration",
                table: "Voitures",
                type: "int",
                unicode: false,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "EngineDisplacement",
                table: "Voitures",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "GearBox",
                table: "Voitures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PowerBHP",
                table: "Voitures",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PowerKW",
                table: "Voitures",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TorqueLBFT",
                table: "Voitures",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TorqueNM",
                table: "Voitures",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Voitures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "WeightLBS",
                table: "Voitures",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "RequiredDLC",
                table: "OriginalCar",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "PictureLink",
                table: "OriginalCar",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false);

            migrationBuilder.AddColumn<string>(
                name: "Aspiration",
                table: "OriginalCar",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EngineConfiguration",
                table: "OriginalCar",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "EngineDisplacement",
                table: "OriginalCar",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "GearBox",
                table: "OriginalCar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PowerBHP",
                table: "OriginalCar",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PowerKW",
                table: "OriginalCar",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TorqueLBFT",
                table: "OriginalCar",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TorqueNM",
                table: "OriginalCar",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "OriginalCar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "WeightLBS",
                table: "OriginalCar",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
