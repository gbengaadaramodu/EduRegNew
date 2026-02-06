using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCourseRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrations_CourseSchedule_CourseScheduleId",
                table: "CourseRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrations_CourseScheduleId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "CourseScheduleId",
                table: "CourseRegistrations");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgrammeCode",
                table: "CourseRegistrations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "CourseRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "CourseRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "CourseRegistrationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CourseScheduleId",
                table: "CourseRegistrationDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "CourseRegistrationDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentsId",
                table: "CourseRegistrationDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrationDetails_CourseScheduleId",
                table: "CourseRegistrationDetails",
                column: "CourseScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrationDetails_StudentsId",
                table: "CourseRegistrationDetails",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrationDetails_AspNetUsers_StudentsId",
                table: "CourseRegistrationDetails",
                column: "StudentsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrationDetails_CourseSchedule_CourseScheduleId",
                table: "CourseRegistrationDetails",
                column: "CourseScheduleId",
                principalTable: "CourseSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrationDetails_AspNetUsers_StudentsId",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseRegistrationDetails_CourseSchedule_CourseScheduleId",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrationDetails_CourseScheduleId",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropIndex(
                name: "IX_CourseRegistrationDetails_StudentsId",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "ProgrammeCode",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "CourseRegistrations");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropColumn(
                name: "CourseScheduleId",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "CourseRegistrationDetails");

            migrationBuilder.DropColumn(
                name: "StudentsId",
                table: "CourseRegistrationDetails");

            migrationBuilder.AddColumn<long>(
                name: "CourseScheduleId",
                table: "CourseRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_CourseScheduleId",
                table: "CourseRegistrations",
                column: "CourseScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRegistrations_CourseSchedule_CourseScheduleId",
                table: "CourseRegistrations",
                column: "CourseScheduleId",
                principalTable: "CourseSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
