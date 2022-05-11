using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jobsity.Chat.Data.Migrations
{
    public partial class AddIntegrationToChatRoomMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ChatRoomsMessages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "Integration",
                table: "ChatRoomsMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Integration",
                table: "ChatRoomsMessages");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ChatRoomsMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
