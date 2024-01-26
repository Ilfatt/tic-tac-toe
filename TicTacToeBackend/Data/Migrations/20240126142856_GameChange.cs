using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class GameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_Player1Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_Player2Id",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Player1Id",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Player2Id",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Player2Id",
                table: "Games",
                newName: "OpponentId");

            migrationBuilder.RenameColumn(
                name: "MaxRating",
                table: "Games",
                newName: "MinRate");

            migrationBuilder.RenameColumn(
                name: "Map",
                table: "Games",
                newName: "GameMap");

            migrationBuilder.AddColumn<int>(
                name: "GameState",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxRate",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Games",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameState",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MaxRate",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "OpponentId",
                table: "Games",
                newName: "Player2Id");

            migrationBuilder.RenameColumn(
                name: "MinRate",
                table: "Games",
                newName: "MaxRating");

            migrationBuilder.RenameColumn(
                name: "GameMap",
                table: "Games",
                newName: "Map");

            migrationBuilder.AddColumn<Guid>(
                name: "Player1Id",
                table: "Games",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player1Id",
                table: "Games",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player2Id",
                table: "Games",
                column: "Player2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_Player1Id",
                table: "Games",
                column: "Player1Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_Player2Id",
                table: "Games",
                column: "Player2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
