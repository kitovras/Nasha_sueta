using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurFuss.Data.Postgre.Migrations
{
    /// <inheritdoc />
    public partial class AddDeferredPublication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeferredPublication",
                schema: "evnt",
                table: "UserEventCustom",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventStatus",
                schema: "evnt",
                table: "UserEventCustom",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeferredPublication",
                schema: "evnt",
                table: "UserEventCustom");

            migrationBuilder.DropColumn(
                name: "EventStatus",
                schema: "evnt",
                table: "UserEventCustom");
        }
    }
}
