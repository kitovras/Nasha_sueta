using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurFuss.Data.Postgre.Migrations
{
    /// <inheritdoc />
    public partial class Q2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "evnt");

            migrationBuilder.CreateTable(
                name: "UserEventCustom",
                schema: "evnt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventCustom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCustomPhoto",
                schema: "evnt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEventCustomId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    FileUniqueId = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCustomPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventCustomPhoto_UserEventCustom_UserEventCustomId",
                        column: x => x.UserEventCustomId,
                        principalSchema: "evnt",
                        principalTable: "UserEventCustom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCustomPhoto_UserEventCustomId",
                schema: "evnt",
                table: "EventCustomPhoto",
                column: "UserEventCustomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventCustomPhoto",
                schema: "evnt");

            migrationBuilder.DropTable(
                name: "UserEventCustom",
                schema: "evnt");
        }
    }
}
