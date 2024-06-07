using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Akademik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResidentsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentsDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    NumberOfBeds = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    PESEL = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncodedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidentDetailsId = table.Column<int>(type: "int", nullable: true),
                    RoomNumberId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.PESEL);
                    table.ForeignKey(
                        name: "FK_Residents_ResidentsDetails_ResidentDetailsId",
                        column: x => x.ResidentDetailsId,
                        principalTable: "ResidentsDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Residents_Rooms_RoomNumberId",
                        column: x => x.RoomNumberId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Residents_ResidentDetailsId",
                table: "Residents",
                column: "ResidentDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_RoomNumberId",
                table: "Residents",
                column: "RoomNumberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "ResidentsDetails");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
