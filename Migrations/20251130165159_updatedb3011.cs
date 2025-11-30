using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class updatedb3011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSignUps");

            migrationBuilder.DropColumn(
                name: "Programme",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AdminUserHasChangePassword",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdminUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsLock",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SelectedCategory",
                table: "AspNetUsers",
                newName: "ProgrammeCode");

            migrationBuilder.RenameColumn(
                name: "ProgramTypeId",
                table: "AspNetUsers",
                newName: "CurrentLevel");

            migrationBuilder.RenameColumn(
                name: "ApplicationBatchId",
                table: "AspNetUsers",
                newName: "AdmittedSessionId");

            migrationBuilder.AddColumn<string>(
                name: "InstitutionShortName",
                table: "Programmes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionShortName",
                table: "Faculties",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionShortName",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AdmittedLevelId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicantId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchShortName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepartmentCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionShortName",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatricNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstitutionShortName",
                table: "AcademicSessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LevelName",
                table: "AcademicLevels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ClassCode",
                table: "AcademicLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseAdviser",
                table: "AcademicLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgrammeCode",
                table: "AcademicLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdmissionBatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BatchShortName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BatchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionBatches", x => new { x.Id, x.BatchShortName });
                });

            migrationBuilder.CreateTable(
                name: "CourseSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prerequisite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LMSId = table.Column<int>(type: "int", nullable: false),
                    IsOnLMS = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prerequisite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Institutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutions", x => new { x.Id, x.InstitutionShortName });
                });

            migrationBuilder.CreateTable(
                name: "ProgramCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prerequisite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatricNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Units = table.Column<int>(type: "int", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    RawScore = table.Column<double>(type: "float", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMSId = table.Column<int>(type: "int", nullable: false),
                    IsEnrolledOnLMS = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationsBusinessRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalCompulsoryUnits = table.Column<int>(type: "int", nullable: false),
                    TotalElectiveUnits = table.Column<int>(type: "int", nullable: false),
                    TotalMinimumCreditUnits = table.Column<int>(type: "int", nullable: false),
                    TotalMaximumCreditUnits = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationsBusinessRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    SemesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FeeCategory = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FeeRecurrenceType = table.Column<int>(type: "int", nullable: false),
                    FeeApplicabilityScope = table.Column<int>(type: "int", nullable: false),
                    IsSystemDefined = table.Column<bool>(type: "bit", nullable: false),
                    ProgrammeFeeScheduleId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeeRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeItemId = table.Column<int>(type: "int", nullable: false),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemesterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecurrenceType = table.Column<int>(type: "int", nullable: false),
                    ApplicabilityScope = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeRule_FeeItem_FeeItemId",
                        column: x => x.FeeItemId,
                        principalTable: "FeeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentFeeSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatricNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FeeItemId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFeeSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentFeeSchedule_FeeItem_FeeItemId",
                        column: x => x.FeeItemId,
                        principalTable: "FeeItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeFeeSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionShortName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProgrammeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    SemesterId = table.Column<int>(type: "int", nullable: true),
                    CourseCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeItemId = table.Column<int>(type: "int", nullable: false),
                    FeeRuleId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeFeeSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgrammeFeeSchedule_FeeRule_FeeRuleId",
                        column: x => x.FeeRuleId,
                        principalTable: "FeeRule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentFeeItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentFeeScheduleId = table.Column<int>(type: "int", nullable: false),
                    FeeItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFeeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentFeeItem_StudentFeeSchedule_StudentFeeScheduleId",
                        column: x => x.StudentFeeScheduleId,
                        principalTable: "StudentFeeSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programmes_InstitutionShortName",
                table: "Programmes",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_InstitutionShortName",
                table: "Faculties",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_InstitutionShortName",
                table: "Departments",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_InstitutionShortName",
                table: "AspNetUsers",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_InstitutionShortName",
                table: "AcademicSessions",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionBatches_InstitutionShortName",
                table: "AdmissionBatches",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSchedule_InstitutionShortName",
                table: "CourseSchedule",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCourses_InstitutionShortName",
                table: "DepartmentCourses",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_FeeItem_InstitutionShortName",
                table: "FeeItem",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_FeeItem_Name_InstitutionShortName",
                table: "FeeItem",
                columns: new[] { "Name", "InstitutionShortName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeeItem_ProgrammeFeeScheduleId",
                table: "FeeItem",
                column: "ProgrammeFeeScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeRule_FeeItemId",
                table: "FeeRule",
                column: "FeeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeRule_InstitutionShortName",
                table: "FeeRule",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_Institutions_InstitutionShortName",
                table: "Institutions",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramCourses_InstitutionShortName",
                table: "ProgramCourses",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeFeeSchedule_FeeRuleId",
                table: "ProgrammeFeeSchedule",
                column: "FeeRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeFeeSchedule_InstitutionShortName",
                table: "ProgrammeFeeSchedule",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_InstitutionShortName",
                table: "Registrations",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationsBusinessRules_InstitutionShortName",
                table: "RegistrationsBusinessRules",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_InstitutionShortName",
                table: "Semesters",
                column: "InstitutionShortName");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeeItem_StudentFeeScheduleId",
                table: "StudentFeeItem",
                column: "StudentFeeScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeeSchedule_FeeItemId",
                table: "StudentFeeSchedule",
                column: "FeeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFeeSchedule_InstitutionShortName",
                table: "StudentFeeSchedule",
                column: "InstitutionShortName");

            migrationBuilder.AddForeignKey(
                name: "FK_FeeItem_ProgrammeFeeSchedule_ProgrammeFeeScheduleId",
                table: "FeeItem",
                column: "ProgrammeFeeScheduleId",
                principalTable: "ProgrammeFeeSchedule",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeeItem_ProgrammeFeeSchedule_ProgrammeFeeScheduleId",
                table: "FeeItem");

            migrationBuilder.DropTable(
                name: "AdmissionBatches");

            migrationBuilder.DropTable(
                name: "CourseSchedule");

            migrationBuilder.DropTable(
                name: "DepartmentCourses");

            migrationBuilder.DropTable(
                name: "Institutions");

            migrationBuilder.DropTable(
                name: "ProgramCourses");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "RegistrationsBusinessRules");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "StudentFeeItem");

            migrationBuilder.DropTable(
                name: "StudentFeeSchedule");

            migrationBuilder.DropTable(
                name: "ProgrammeFeeSchedule");

            migrationBuilder.DropTable(
                name: "FeeRule");

            migrationBuilder.DropTable(
                name: "FeeItem");

            migrationBuilder.DropIndex(
                name: "IX_Programmes_InstitutionShortName",
                table: "Programmes");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_InstitutionShortName",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Departments_InstitutionShortName",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_InstitutionShortName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_InstitutionShortName",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "InstitutionShortName",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "InstitutionShortName",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AdmittedLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BatchShortName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstitutionShortName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MatricNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstitutionShortName",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "ClassCode",
                table: "AcademicLevels");

            migrationBuilder.DropColumn(
                name: "CourseAdviser",
                table: "AcademicLevels");

            migrationBuilder.DropColumn(
                name: "ProgrammeCode",
                table: "AcademicLevels");

            migrationBuilder.RenameColumn(
                name: "ProgrammeCode",
                table: "AspNetUsers",
                newName: "SelectedCategory");

            migrationBuilder.RenameColumn(
                name: "CurrentLevel",
                table: "AspNetUsers",
                newName: "ProgramTypeId");

            migrationBuilder.RenameColumn(
                name: "AdmittedSessionId",
                table: "AspNetUsers",
                newName: "ApplicationBatchId");

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionShortName",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Programme",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AdminUserHasChangePassword",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdminUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLock",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LevelName",
                table: "AcademicLevels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StudentSignUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    ActiveStatus = table.Column<int>(type: "int", nullable: false),
                    AdmittedLevelId = table.Column<int>(type: "int", nullable: false),
                    AdmittedSessionId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationBatchId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentAcademicSessionId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsLock = table.Column<bool>(type: "bit", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatricNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    ProgramTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSignUps", x => x.Id);
                });
        }
    }
}
