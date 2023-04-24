using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace German.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class usercourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorCourseLessons_Authors_AuthorId",
                table: "AuthorCourseLessons");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorCourseLessons_CourseLessons_CourseLessonId",
                table: "AuthorCourseLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorCourseLessons",
                table: "AuthorCourseLessons");

            migrationBuilder.DropIndex(
                name: "IX_AuthorCourseLessons_CourseLessonId",
                table: "AuthorCourseLessons");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "authorid",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_authorid",
                table: "Courses",
                column: "authorid");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Authors_authorid",
                table: "Courses",
                column: "authorid",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Authors_authorid",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_authorid",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "authorid",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorCourseLessons",
                table: "AuthorCourseLessons",
                columns: new[] { "AuthorId", "CourseLessonId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorCourseLessons_CourseLessonId",
                table: "AuthorCourseLessons",
                column: "CourseLessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorCourseLessons_Authors_AuthorId",
                table: "AuthorCourseLessons",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorCourseLessons_CourseLessons_CourseLessonId",
                table: "AuthorCourseLessons",
                column: "CourseLessonId",
                principalTable: "CourseLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
