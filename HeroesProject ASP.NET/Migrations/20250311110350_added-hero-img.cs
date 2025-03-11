using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroesProject_ASP.NET.Migrations
{
    /// <inheritdoc />
    public partial class addedheroimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Heroes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Heroes");
        }
    }
}
