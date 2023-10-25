using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurFuss.Core.Migrations
{
    public partial class AddNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tg");

            migrationBuilder.CreateTable(
                name: "TelegramAccount",
                schema: "tg",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chatId = table.Column<long>(type: "bigint", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    regionCodeSelected = table.Column<int>(type: "int", nullable: false),
                    dateTimeAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramAccount", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramAccount",
                schema: "tg");
        }
    }
}
