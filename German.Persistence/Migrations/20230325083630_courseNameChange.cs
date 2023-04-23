using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace German.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class courseNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "CourseLessons",
                newName: "LessonName");

            migrationBuilder.RenameColumn(
                name: "CourseDescription",
                table: "CourseLessons",
                newName: "LessonDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LessonName",
                table: "CourseLessons",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "LessonDescription",
                table: "CourseLessons",
                newName: "CourseDescription");
        }
    }
}
