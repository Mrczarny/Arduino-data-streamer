using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arduino_data_streamer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataModels",
                columns: table => new
                {
                    BotId = table.Column<string>(type: "TEXT", nullable: false),
                    SensorFrontDistance = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorLeftDistance = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorRightDistance = table.Column<int>(type: "INTEGER", nullable: false),
                    MotorsSpeed = table.Column<int>(type: "INTEGER", nullable: false),
                    IsOnLine = table.Column<bool>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataModels");
        }
    }
}
