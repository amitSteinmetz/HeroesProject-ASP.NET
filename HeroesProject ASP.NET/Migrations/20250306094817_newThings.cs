﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroesProject_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class newThings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_AspNetUsers_UserId",
                table: "Trainers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_AspNetUsers_UserId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_UserId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Trainers");
        }
    }
}
