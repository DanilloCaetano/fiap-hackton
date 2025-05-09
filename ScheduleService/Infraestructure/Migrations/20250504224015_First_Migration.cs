using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class First_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CRM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Specialty = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchedulesDoctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAllocated = table.Column<bool>(type: "bit", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulesDoctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulesDoctor_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleDoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CancellationJustification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_SchedulesDoctor_ScheduleDoctorId",
                        column: x => x.ScheduleDoctorId,
                        principalTable: "SchedulesDoctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_PatientId",
                table: "Schedules",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ScheduleDoctorId",
                table: "Schedules",
                column: "ScheduleDoctorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesDoctor_DoctorId",
                table: "SchedulesDoctor",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "SchedulesDoctor");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DDD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "Active", "CreationDate", "DDD", "Description" },
                values: new object[,]
                {
                    { new Guid("01378b5d-fdc4-4435-b595-6cfad33ef4a6"), true, new DateTime(2024, 8, 11, 18, 23, 16, 158, DateTimeKind.Utc).AddTicks(448), "21", "Rio de Janeiro" },
                    { new Guid("cefa4129-b1d8-4eb5-b278-86d86d8763c2"), true, new DateTime(2024, 8, 11, 18, 23, 16, 158, DateTimeKind.Utc).AddTicks(323), "11", "São Paulo" },
                    { new Guid("efe16b65-feca-402a-8e21-2d1843f0d313"), true, new DateTime(2024, 8, 11, 18, 23, 16, 158, DateTimeKind.Utc).AddTicks(293), "13", "Baixada Santista" }
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Active", "CellphoneNumber", "CreationDate", "Email", "Name", "RegionId" },
                values: new object[,]
                {
                    { new Guid("18887b4a-437d-4c62-949c-00c4a890e35a"), true, "123456789", new DateTime(2024, 8, 11, 18, 23, 16, 157, DateTimeKind.Utc).AddTicks(7283), "carlospereira@example.com", "Carlos Pereira", new Guid("01378b5d-fdc4-4435-b595-6cfad33ef4a6") },
                    { new Guid("39b557cf-682e-4437-8c4b-db19ee52f25d"), true, "123456789", new DateTime(2024, 8, 11, 18, 23, 16, 157, DateTimeKind.Utc).AddTicks(7249), "joaosilva@example.com", "João Silva", new Guid("efe16b65-feca-402a-8e21-2d1843f0d313") },
                    { new Guid("d9f37035-4479-4dba-a3b6-f5c918e21736"), true, "123456789", new DateTime(2024, 8, 11, 18, 23, 16, 157, DateTimeKind.Utc).AddTicks(7274), "mariaoliveira@example.com", "Maria Oliveira", new Guid("cefa4129-b1d8-4eb5-b278-86d86d8763c2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_RegionId",
                table: "Contact",
                column: "RegionId");
        }
    }
}
