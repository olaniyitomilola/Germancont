using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace German.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_users_UserID",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_UserID",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                table: "CourseLessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CourseDescription",
                table: "CourseLessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                table: "CourseLessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CourseDescription",
                table: "CourseLessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserID",
                table: "Authors",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_users_UserID",
                table: "Authors",
                column: "UserID",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
