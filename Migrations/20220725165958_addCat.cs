using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserApi.Migrations
{
    public partial class addCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OriginalCar");

            migrationBuilder.DropTable(
                name: "Maker");

            migrationBuilder.CreateTable(
                name: "Maker",
                columns: table => new
                {
                    Id_Maker = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Origin = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Founded = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Related = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    WikiLink = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maker", x => x.Id_Maker);
                });

            migrationBuilder.CreateTable(
                name: "OriginalCar",
                columns: table => new
                {
                    Id_Car = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    CarOrdinal = table.Column<int>(type: "int", nullable: false),
                    Maker_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PowerBHP = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PowerKW = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    TorqueLBFT = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    TorqueNM = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    WeightLBS = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    WeightKG = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    EngineDisplacement = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NbCylindre = table.Column<int>(type: "int", nullable: false),
                    Aspiration = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    EngineConfiguration = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    EnginePosition = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Speed = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Handling = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Accelerate = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Launch = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Braking = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Offroad = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PI = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RequiredDLC = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Aviability = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Rarity = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    WikiLink = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PictureLink = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    GearBox = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_1", x => x.Id_Car);
                    table.ForeignKey(
                        name: "FK_Car_Maker",
                        column: x => x.Maker_Id,
                        principalTable: "Maker",
                        principalColumn: "Id_Maker");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_Maker_Id",
                table: "OriginalCar",
                column: "Maker_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OriginalCar");

            migrationBuilder.DropTable(
                name: "Maker");
        }
    }
}
