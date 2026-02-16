using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class MatricNumberToTick1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Ticketing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Ticketing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
