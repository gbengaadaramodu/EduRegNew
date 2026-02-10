using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class ModfiedSemesters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Semesters");

            migrationBuilder.AddColumn<string>(
                name: "BatchShortName",
                table: "Semesters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationCloseDate",
                table: "Semesters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationEndDate",
                table: "Semesters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationStartDate",
                table: "Semesters",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchShortName",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "RegistrationCloseDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "RegistrationEndDate",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "RegistrationStartDate",
                table: "Semesters");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Semesters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
