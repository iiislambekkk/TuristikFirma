using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuristikFirma.Migrations
{
    /// <inheritdoc />
    public partial class daysDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DaysEn",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DaysKz",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DaysRu",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysEn",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DaysKz",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "DaysRu",
                table: "Tours");
        }
    }
}
