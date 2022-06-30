using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TFWebService.Data.Migrations
{
    public partial class adddeviceandotherchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainDetails_Users_UserId1",
                table: "MainDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackDetails_Users_UserId1",
                table: "TrackDetails");

            migrationBuilder.DropIndex(
                name: "IX_TrackDetails_UserId1",
                table: "TrackDetails");

            migrationBuilder.DropIndex(
                name: "IX_MainDetails_UserId1",
                table: "MainDetails");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TrackDetails");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MainDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TrackDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MainDetails",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceID = table.Column<string>(type: "TEXT", nullable: true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    BuildID = table.Column<string>(type: "TEXT", nullable: true),
                    SDK = table.Column<string>(type: "TEXT", nullable: true),
                    Manufacture = table.Column<string>(type: "TEXT", nullable: true),
                    BuildUser = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Base = table.Column<string>(type: "TEXT", nullable: true),
                    Incremental = table.Column<string>(type: "TEXT", nullable: true),
                    Board = table.Column<string>(type: "TEXT", nullable: true),
                    Host = table.Column<string>(type: "TEXT", nullable: true),
                    FingerPrint = table.Column<string>(type: "TEXT", nullable: true),
                    VersionCode = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackDetails_UserId",
                table: "TrackDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainDetails_UserId",
                table: "MainDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MainDetails_Users_UserId",
                table: "MainDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackDetails_Users_UserId",
                table: "TrackDetails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainDetails_Users_UserId",
                table: "MainDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackDetails_Users_UserId",
                table: "TrackDetails");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_TrackDetails_UserId",
                table: "TrackDetails");

            migrationBuilder.DropIndex(
                name: "IX_MainDetails_UserId",
                table: "MainDetails");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TrackDetails",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "TrackDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MainDetails",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "MainDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackDetails_UserId1",
                table: "TrackDetails",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MainDetails_UserId1",
                table: "MainDetails",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MainDetails_Users_UserId1",
                table: "MainDetails",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackDetails_Users_UserId1",
                table: "TrackDetails",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
