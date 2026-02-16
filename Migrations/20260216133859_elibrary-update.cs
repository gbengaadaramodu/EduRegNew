using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class elibraryupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FeeRule_FeeItem_FeeItemId",
            //    table: "FeeRule");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "ELibraries",
                newName: "FileUrl");

            migrationBuilder.AlterColumn<long>(
                name: "FeeItemId",
                table: "FeeRule",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "ELibraries",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionShortName",
                table: "ELibraries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CourseCode",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeBytes",
                table: "ELibraries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ELibraries_InstitutionShortName",
                table: "ELibraries",
                column: "InstitutionShortName");

            migrationBuilder.AddForeignKey(
                name: "FK_FeeRule_FeeItem_FeeItemId",
                table: "FeeRule",
                column: "FeeItemId",
                principalTable: "FeeItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FeeRule_FeeItem_FeeItemId",
            //    table: "FeeRule");

            migrationBuilder.DropIndex(
                name: "IX_ELibraries_InstitutionShortName",
                table: "ELibraries");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "ELibraries");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ELibraries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ELibraries");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ELibraries");

            migrationBuilder.DropColumn(
                name: "FileSizeBytes",
                table: "ELibraries");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ELibraries");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "ELibraries",
                newName: "FilePath");

            migrationBuilder.AlterColumn<long>(
                name: "FeeItemId",
                table: "FeeRule",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProgramId",
                table: "ELibraries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionShortName",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CourseCode",
                table: "ELibraries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeeRule_FeeItem_FeeItemId",
                table: "FeeRule",
                column: "FeeItemId",
                principalTable: "FeeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
