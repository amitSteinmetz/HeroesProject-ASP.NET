﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroesProject_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class changeHeroDayly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DailyTrainingCount",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DailyTrainingCount",
                table: "Heroes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
