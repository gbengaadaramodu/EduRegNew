using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class CourseMaxMinAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionSemesterId",
                table: "SessionSemesters");

            migrationBuilder.CreateTable(
                name: "CourseMaxMin",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumUnits = table.Column<int>(type: "int", nullable: false),
                    MaximumUnits = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMaxMin", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseMaxMin");

            migrationBuilder.AddColumn<int>(
                name: "SessionSemesterId",
                table: "SessionSemesters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
