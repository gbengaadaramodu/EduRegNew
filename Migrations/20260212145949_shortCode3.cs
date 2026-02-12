using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class shortCode3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AspNetUsers",
                newName: "StartClassCode");

            migrationBuilder.AddColumn<string>(
                name: "CurrentClassCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentClassCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "StartClassCode",
                table: "AspNetUsers",
                newName: "Password");
        }
    }
}
