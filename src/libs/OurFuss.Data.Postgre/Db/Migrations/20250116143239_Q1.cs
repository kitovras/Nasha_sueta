using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurFuss.Data.Postgre.Migrations
{
    /// <inheritdoc />
    public partial class Q1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TelegramId",
                schema: "tg",
                table: "Account",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramId",
                schema: "tg",
                table: "Account");
        }
    }
}
