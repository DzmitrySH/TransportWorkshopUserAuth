using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportWorkshopUserAuth.Data.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirsLastMidName = table.Column<string>(maxLength: 30, nullable: false),
                    Category = table.Column<int>(nullable: false),
                    RightsNumber = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "NormaFuel",
                columns: table => new
                {
                    NormaFuelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Linear = table.Column<double>(nullable: false),
                    Summer = table.Column<double>(nullable: false),
                    Winter = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormaFuel", x => x.NormaFuelId);
                });

            migrationBuilder.CreateTable(
                name: "Tire",
                columns: table => new
                {
                    TireId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Brand = table.Column<string>(maxLength: 20, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    RunStart = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tire", x => x.TireId);
                });

            migrationBuilder.CreateTable(
                name: "TypeAuto",
                columns: table => new
                {
                    TypeAutoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameType = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAuto", x => x.TypeAutoId);
                });

            migrationBuilder.CreateTable(
                name: "TypeFuel",
                columns: table => new
                {
                    TypeFuelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fuel = table.Column<string>(maxLength: 20, nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeFuel", x => x.TypeFuelId);
                });

            migrationBuilder.CreateTable(
                name: "WinterTime",
                columns: table => new
                {
                    WinterTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinterNorma = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinterTime", x => x.WinterTimeId);
                });

            migrationBuilder.CreateTable(
                name: "Trailer",
                columns: table => new
                {
                    TrailerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(maxLength: 20, nullable: false),
                    Massa = table.Column<int>(nullable: false),
                    TireId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trailer", x => x.TrailerId);
                    table.ForeignKey(
                        name: "FK_Trailer_Tire_TireId",
                        column: x => x.TireId,
                        principalTable: "Tire",
                        principalColumn: "TireId",
                        onDelete: ReferentialAction.NoAction);//Cascade
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namedevice = table.Column<string>(maxLength: 30, nullable: false),
                    TypeFuelId = table.Column<int>(nullable: false),
                    SumerTime = table.Column<DateTime>(nullable: false),
                    WinterTimeId = table.Column<int>(nullable: false),
                    Harmfulness = table.Column<bool>(nullable: false),
                    TireId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_Tire_TireId",
                        column: x => x.TireId,
                        principalTable: "Tire",
                        principalColumn: "TireId",
                        onDelete: ReferentialAction.NoAction);//Cascade
                    table.ForeignKey(
                        name: "FK_Device_TypeFuel_TypeFuelId",
                        column: x => x.TypeFuelId,
                        principalTable: "TypeFuel",
                        principalColumn: "TypeFuelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_WinterTime_WinterTimeId",
                        column: x => x.WinterTimeId,
                        principalTable: "WinterTime",
                        principalColumn: "WinterTimeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutoCar",
                columns: table => new
                {
                    AutoCarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAuto = table.Column<string>(maxLength: 30, nullable: false),
                    Number = table.Column<string>(maxLength: 12, nullable: false),
                    Mileage = table.Column<int>(nullable: false),
                    TypeFuelId = table.Column<int>(nullable: false),
                    NormaFuelId = table.Column<int>(nullable: false),
                    TrailerId = table.Column<int>(nullable: false),
                    DriverId = table.Column<int>(nullable: false),
                    TypeAutoId = table.Column<int>(nullable: false),
                    TireId = table.Column<int>(nullable: false),
                    Harmfulness = table.Column<int>(nullable: false),
                    Navigation = table.Column<bool>(nullable: false),
                    Injector = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoCar", x => x.AutoCarId);
                    table.ForeignKey(
                        name: "FK_AutoCar_Driver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Driver",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoCar_NormaFuel_NormaFuelId",
                        column: x => x.NormaFuelId,
                        principalTable: "NormaFuel",
                        principalColumn: "NormaFuelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoCar_Tire_TireId",
                        column: x => x.TireId,
                        principalTable: "Tire",
                        principalColumn: "TireId",
                        onDelete: ReferentialAction.NoAction);//Cascade
                    table.ForeignKey(
                        name: "FK_AutoCar_Trailer_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "Trailer",
                        principalColumn: "TrailerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoCar_TypeAuto_TypeAutoId",
                        column: x => x.TypeAutoId,
                        principalTable: "TypeAuto",
                        principalColumn: "TypeAutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoCar_TypeFuel_TypeFuelId",
                        column: x => x.TypeFuelId,
                        principalTable: "TypeFuel",
                        principalColumn: "TypeFuelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    BalanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    AutoCarId = table.Column<int>(nullable: false),
                    Leftovers = table.Column<int>(nullable: false),
                    Sug = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balance", x => x.BalanceId);
                    table.ForeignKey(
                        name: "FK_Balance_AutoCar_AutoCarId",
                        column: x => x.AutoCarId,
                        principalTable: "AutoCar",
                        principalColumn: "AutoCarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                columns: table => new
                {
                    MaintenanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeTO = table.Column<string>(maxLength: 10, nullable: false),
                    AutoCarId = table.Column<int>(nullable: false),
                    DateTO = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_Maintenance_AutoCar_AutoCarId",
                        column: x => x.AutoCarId,
                        principalTable: "AutoCar",
                        principalColumn: "AutoCarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoCar_DriverId",
                table: "AutoCar",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoCar_NormaFuelId",
                table: "AutoCar",
                column: "NormaFuelId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoCar_TireId",
                table: "AutoCar",
                column: "TireId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoCar_TrailerId",
                table: "AutoCar",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoCar_TypeAutoId",
                table: "AutoCar",
                column: "TypeAutoId");

            migrationBuilder.CreateIndex(
                name: "IX_AutoCar_TypeFuelId",
                table: "AutoCar",
                column: "TypeFuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Balance_AutoCarId",
                table: "Balance",
                column: "AutoCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_TireId",
                table: "Device",
                column: "TireId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_TypeFuelId",
                table: "Device",
                column: "TypeFuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_WinterTimeId",
                table: "Device",
                column: "WinterTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_AutoCarId",
                table: "Maintenance",
                column: "AutoCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Trailer_TireId",
                table: "Trailer",
                column: "TireId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Maintenance");

            migrationBuilder.DropTable(
                name: "WinterTime");

            migrationBuilder.DropTable(
                name: "AutoCar");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "NormaFuel");

            migrationBuilder.DropTable(
                name: "Trailer");

            migrationBuilder.DropTable(
                name: "TypeAuto");

            migrationBuilder.DropTable(
                name: "TypeFuel");

            migrationBuilder.DropTable(
                name: "Tire");
        }
    }
}
