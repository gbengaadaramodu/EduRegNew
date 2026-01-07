using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class Addini0501 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammeFeeSchedule_FeeRule_FeeRuleId",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFeeItem_StudentFeeSchedule_StudentFeeScheduleId",
                table: "StudentFeeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFeeSchedule_FeeItem_FeeItemId",
                table: "StudentFeeSchedule");

            migrationBuilder.DropIndex(
                name: "IX_StudentFeeItem_StudentFeeScheduleId",
                table: "StudentFeeItem");

            migrationBuilder.DropIndex(
                name: "IX_ProgrammeFeeSchedule_FeeRuleId",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentFeeSchedule",
                table: "StudentFeeSchedule");

            migrationBuilder.AlterColumn<long>(
                name: "FeeItemId",
                table: "StudentFeeSchedule",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "StudentFeeSchedule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentFeeSchedule",
                table: "StudentFeeSchedule",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentFeeItem",
                table: "StudentFeeItem");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "StudentFeeItem",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentFeeItem",
                table: "StudentFeeItem",
                column: "Id");

            migrationBuilder.AddColumn<long>(
                name: "StudentFeeScheduleId1",
                table: "StudentFeeItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Semesters",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationsBusinessRules",
                table: "RegistrationsBusinessRules");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "RegistrationsBusinessRules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationsBusinessRules",
                table: "RegistrationsBusinessRules",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Registrations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programmes",
                table: "Programmes");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Programmes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programmes",
                table: "Programmes",
                column: "Id");

            migrationBuilder.DropForeignKey(
                name: "FK_FeeItem_ProgrammeFeeSchedule_ProgrammeFeeScheduleId",
                table: "FeeItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgrammeFeeSchedule",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.AlterColumn<long>(
                name: "FeeItemId",
                table: "ProgrammeFeeSchedule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProgrammeFeeSchedule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgrammeFeeSchedule",
                table: "ProgrammeFeeSchedule",
                column: "Id");

            migrationBuilder.AddColumn<long>(
                name: "FeeRuleId1",
                table: "ProgrammeFeeSchedule",
                type: "bigint",
                nullable: true);

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramCourses",
                table: "ProgramCourses");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProgramCourses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramCourses",
                table: "ProgramCourses",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Institutions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions",
                columns: new[] { "Id", "InstitutionShortName" });

            migrationBuilder.DropForeignKey(
                name: "FK_FeeRule_FeeItem_FeeItemId",
                table: "FeeRule");

            migrationBuilder.AlterColumn<long>(
                name: "FeeItemId",
                table: "FeeRule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeRule",
                table: "FeeRule");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "FeeRule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeRule",
                table: "FeeRule",
                column: "Id");

            migrationBuilder.AlterColumn<long>(
                name: "ProgrammeFeeScheduleId",
                table: "FeeItem",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeItem",
                table: "FeeItem");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "FeeItem",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeItem",
                table: "FeeItem",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Faculties",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Departments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentCourses",
                table: "DepartmentCourses");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "DepartmentCourses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentCourses",
                table: "DepartmentCourses",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSchedule",
                table: "CourseSchedule");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CourseSchedule",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSchedule",
                table: "CourseSchedule",
                column: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CurrentSemesterId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentSessionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdmissionBatches",
                table: "AdmissionBatches");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AdmissionBatches",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdmissionBatches",
                table: "AdmissionBatches",
                columns: new[] { "Id", "BatchShortName" });

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessions",
                table: "AcademicSessions");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AcademicSessions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessions",
                table: "AcademicSessions",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicLevels",
                table: "AcademicLevels");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AcademicLevels",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicLevels",
                table: "AcademicLevels",
                column: "Id");

            migrationBuilder.AddColumn<string>(
                name: "InstitutionShortName",
                table: "AcademicLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AcademicLevels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseRegistrations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_AspNetUsers_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRegistrations_CourseSchedule_CourseScheduleId",
                        column: x => x.CourseScheduleId,
                        principalTable: "CourseSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRegistrationDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseRegistrationId = table.Column<long>(type: "bigint", nullable: false),
                    CA = table.Column<double>(type: "float", nullable: true),
                    ExamScore = table.Column<double>(type: "float", nullable: true),
                    HasRegisteredForExam = table.Column<bool>(type: "bit", nullable: false),
                    ExamRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExamStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCarryOver = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegistrationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRegistrationDetails_CourseRegistrations_CourseRegistrationId",
                        column: x => x.CourseRegistrationId,
                        principalTable: "CourseRegistrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeeItem_StudentFeeScheduleId1",
                table: "StudentFeeItem",
                column: "StudentFeeScheduleId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeFeeSchedule_FeeRuleId1",
                table: "ProgrammeFeeSchedule",
                column: "FeeRuleId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrationDetails_CourseRegistrationId",
                table: "CourseRegistrationDetails",
                column: "CourseRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_CourseScheduleId",
                table: "CourseRegistrations",
                column: "CourseScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegistrations_StudentsId",
                table: "CourseRegistrations",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammeFeeSchedule_FeeRule_FeeRuleId1",
                table: "ProgrammeFeeSchedule",
                column: "FeeRuleId1",
                principalTable: "FeeRule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFeeItem_StudentFeeSchedule_StudentFeeScheduleId1",
                table: "StudentFeeItem",
                column: "StudentFeeScheduleId1",
                principalTable: "StudentFeeSchedule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFeeSchedule_FeeItem_FeeItemId",
                table: "StudentFeeSchedule",
                column: "FeeItemId",
                principalTable: "FeeItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgrammeFeeSchedule_FeeRule_FeeRuleId1",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFeeItem_StudentFeeSchedule_StudentFeeScheduleId1",
                table: "StudentFeeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFeeSchedule_FeeItem_FeeItemId",
                table: "StudentFeeSchedule");

            migrationBuilder.DropTable(
                name: "CourseRegistrationDetails");

            migrationBuilder.DropTable(
                name: "CourseRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_StudentFeeItem_StudentFeeScheduleId1",
                table: "StudentFeeItem");

            migrationBuilder.DropIndex(
                name: "IX_ProgrammeFeeSchedule_FeeRuleId1",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.DropColumn(
                name: "StudentFeeScheduleId1",
                table: "StudentFeeItem");

            migrationBuilder.DropColumn(
                name: "FeeRuleId1",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.DropColumn(
                name: "CurrentSemesterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CurrentSessionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstitutionShortName",
                table: "AcademicLevels");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "AcademicLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentFeeSchedule",
                table: "StudentFeeSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "FeeItemId",
                table: "StudentFeeSchedule",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StudentFeeSchedule",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentFeeSchedule",
                table: "StudentFeeSchedule",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentFeeItem",
                table: "StudentFeeItem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "StudentFeeItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentFeeItem",
                table: "StudentFeeItem",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Semesters",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Semesters",
                table: "Semesters",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationsBusinessRules",
                table: "RegistrationsBusinessRules");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RegistrationsBusinessRules",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationsBusinessRules",
                table: "RegistrationsBusinessRules",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Registrations",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programmes",
                table: "Programmes");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Programmes",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programmes",
                table: "Programmes",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgrammeFeeSchedule",
                table: "ProgrammeFeeSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "FeeItemId",
                table: "ProgrammeFeeSchedule",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProgrammeFeeSchedule",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgrammeFeeSchedule",
                table: "ProgrammeFeeSchedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeeItem_ProgrammeFeeSchedule_ProgrammeFeeScheduleId",
                table: "FeeItem",
                column: "ProgrammeFeeScheduleId",
                principalTable: "ProgrammeFeeSchedule",
                principalColumn: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramCourses",
                table: "ProgramCourses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ProgramCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramCourses",
                table: "ProgramCourses",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Institutions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Institutions",
                table: "Institutions",
                columns: new[] { "Id", "InstitutionShortName" });

            migrationBuilder.DropForeignKey(
                name: "FK_FeeRule_FeeItem_FeeItemId",
                table: "FeeRule");

            migrationBuilder.AlterColumn<int>(
                name: "FeeItemId",
                table: "FeeRule",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeRule",
                table: "FeeRule");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FeeRule",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeRule",
                table: "FeeRule",
                column: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "ProgrammeFeeScheduleId",
                table: "FeeItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeeItem",
                table: "FeeItem");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FeeItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeeItem",
                table: "FeeItem",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Faculties",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Departments",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentCourses",
                table: "DepartmentCourses");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DepartmentCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentCourses",
                table: "DepartmentCourses",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseSchedule",
                table: "CourseSchedule");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CourseSchedule",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseSchedule",
                table: "CourseSchedule",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdmissionBatches",
                table: "AdmissionBatches");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AdmissionBatches",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdmissionBatches",
                table: "AdmissionBatches",
                columns: new[] { "Id", "BatchShortName" });

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessions",
                table: "AcademicSessions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AcademicSessions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessions",
                table: "AcademicSessions",
                column: "Id");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicLevels",
                table: "AcademicLevels");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AcademicLevels",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicLevels",
                table: "AcademicLevels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeeItem_StudentFeeScheduleId",
                table: "StudentFeeItem",
                column: "StudentFeeScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeFeeSchedule_FeeRuleId",
                table: "ProgrammeFeeSchedule",
                column: "FeeRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgrammeFeeSchedule_FeeRule_FeeRuleId",
                table: "ProgrammeFeeSchedule",
                column: "FeeRuleId",
                principalTable: "FeeRule",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFeeItem_StudentFeeSchedule_StudentFeeScheduleId",
                table: "StudentFeeItem",
                column: "StudentFeeScheduleId",
                principalTable: "StudentFeeSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFeeSchedule_FeeItem_FeeItemId",
                table: "StudentFeeSchedule",
                column: "FeeItemId",
                principalTable: "FeeItem",
                principalColumn: "Id");
        }
    }
}
