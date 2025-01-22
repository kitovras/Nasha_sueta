using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurFuss.Data.Postgre.Migrations
{
    /// <inheritdoc />
    public partial class Q3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TelegramAccountId",
                schema: "evnt",
                table: "UserEventCustom",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserEventCustom_TelegramAccountId",
                schema: "evnt",
                table: "UserEventCustom",
                column: "TelegramAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEventCustom_Account_TelegramAccountId",
                schema: "evnt",
                table: "UserEventCustom",
                column: "TelegramAccountId",
                principalSchema: "tg",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEventCustom_Account_TelegramAccountId",
                schema: "evnt",
                table: "UserEventCustom");

            migrationBuilder.DropIndex(
                name: "IX_UserEventCustom_TelegramAccountId",
                schema: "evnt",
                table: "UserEventCustom");

            migrationBuilder.DropColumn(
                name: "TelegramAccountId",
                schema: "evnt",
                table: "UserEventCustom");
        }
    }
}
