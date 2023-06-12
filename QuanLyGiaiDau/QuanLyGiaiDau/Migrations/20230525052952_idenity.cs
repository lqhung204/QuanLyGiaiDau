using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyGiaiDau.Migrations
{
    public partial class idenity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoiDaus",
                columns: table => new
                {
                    IdDoiDau = table.Column<string>(type: "varchar(10)", nullable: false),
                    TenDoiDau = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoiDaus", x => x.IdDoiDau);
                });

            migrationBuilder.CreateTable(
                name: "MonTheThaos",
                columns: table => new
                {
                    IdMonTheThao = table.Column<string>(type: "varchar(10)", nullable: false),
                    TenMonTheThao = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonTheThaos", x => x.IdMonTheThao);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<string>(type: "varchar(10)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "GiaiDaus",
                columns: table => new
                {
                    IdGiaiDau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenGiaiDau = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "Datetime", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    IdMonTheThao = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaiDaus", x => x.IdGiaiDau);
                    table.ForeignKey(
                        name: "FK_GiaiDaus_MonTheThaos_IdMonTheThao",
                        column: x => x.IdMonTheThao,
                        principalTable: "MonTheThaos",
                        principalColumn: "IdMonTheThao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CT_DoiDaus",
                columns: table => new
                {
                    IdCTDoiDau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "varchar(10)", nullable: true),
                    IdDoiDau = table.Column<string>(type: "varchar(10)", nullable: true),
                    TrangThaiTV = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_DoiDaus", x => x.IdCTDoiDau);
                    table.ForeignKey(
                        name: "FK_CT_DoiDaus_DoiDaus_IdDoiDau",
                        column: x => x.IdDoiDau,
                        principalTable: "DoiDaus",
                        principalColumn: "IdDoiDau",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CT_DoiDaus_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CT_TranDaus",
                columns: table => new
                {
                    IdCTTranDau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGiaiDau = table.Column<int>(type: "int", nullable: false),
                    IdDoiDau = table.Column<string>(type: "varchar(10)", nullable: true),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    VongDau = table.Column<int>(type: "int", nullable: false),
                    SanDau = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    TiSo = table.Column<int>(type: "int", nullable: false),
                    KetQua = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_TranDaus", x => x.IdCTTranDau);
                    table.ForeignKey(
                        name: "FK_CT_TranDaus_DoiDaus_IdDoiDau",
                        column: x => x.IdDoiDau,
                        principalTable: "DoiDaus",
                        principalColumn: "IdDoiDau",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CT_TranDaus_GiaiDaus_IdGiaiDau",
                        column: x => x.IdGiaiDau,
                        principalTable: "GiaiDaus",
                        principalColumn: "IdGiaiDau",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_DoiDaus_IdDoiDau",
                table: "CT_DoiDaus",
                column: "IdDoiDau");

            migrationBuilder.CreateIndex(
                name: "IX_CT_DoiDaus_IdUser",
                table: "CT_DoiDaus",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_CT_TranDaus_IdDoiDau",
                table: "CT_TranDaus",
                column: "IdDoiDau");

            migrationBuilder.CreateIndex(
                name: "IX_CT_TranDaus_IdGiaiDau",
                table: "CT_TranDaus",
                column: "IdGiaiDau");

            migrationBuilder.CreateIndex(
                name: "IX_GiaiDaus_IdMonTheThao",
                table: "GiaiDaus",
                column: "IdMonTheThao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_DoiDaus");

            migrationBuilder.DropTable(
                name: "CT_TranDaus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DoiDaus");

            migrationBuilder.DropTable(
                name: "GiaiDaus");

            migrationBuilder.DropTable(
                name: "MonTheThaos");
        }
    }
}
