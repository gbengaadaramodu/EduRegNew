using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduReg.Migrations
{
    /// <inheritdoc />
    public partial class MatricNumberToTick : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FeeRule_FeeItem_FeeItemId",
            //    table: "FeeRule");

            migrationBuilder.AddColumn<string>(
                name: "MatricNumber",
                table: "Ticketing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Ticketing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            //migrationBuilder.AlterColumn<long>(
            //    name: "FeeItemId",
            //    table: "FeeRule",
            //    type: "bigint",
            //    nullable: true,
            //    oldClrType: typeof(long),
            //    oldType: "bigint");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_FeeRule_FeeItem_FeeItemId",
        //        table: "FeeRule",
        //        column: "FeeItemId",
        //        principalTable: "FeeItem",
        //        principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FeeRule_FeeItem_FeeItemId",
            //    table: "FeeRule");

            migrationBuilder.DropColumn(
                name: "MatricNumber",
                table: "Ticketing");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Ticketing");

            //migrationBuilder.AlterColumn<long>(
            //    name: "FeeItemId",
            //    table: "FeeRule",
            //    type: "bigint",
            //    nullable: false,
            //    defaultValue: 0L,
            //    oldClrType: typeof(long),
            //    oldType: "bigint",
            //    oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_FeeRule_FeeItem_FeeItemId",
            //    table: "FeeRule",
            //    column: "FeeItemId",
            //    principalTable: "FeeItem",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
