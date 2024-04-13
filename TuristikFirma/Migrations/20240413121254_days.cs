using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuristikFirma.Migrations
{
    /// <inheritdoc />
    public partial class days : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfDays",
                table: "Tours",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfDays",
                table: "Tours");
        }
    }
}
