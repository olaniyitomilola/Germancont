using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace German.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class courseLessonColumnsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "CourseLessons");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "CourseLessons");

            migrationBuilder.RenameColumn(
                name: "LessonName",
                table: "CourseLessons",
                newName: "LessonTitle");

            migrationBuilder.RenameColumn(
                name: "LessonDescription",
                table: "CourseLessons",
                newName: "LessonParagraphs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LessonTitle",
                table: "CourseLessons",
                newName: "LessonName");

            migrationBuilder.RenameColumn(
                name: "LessonParagraphs",
                table: "CourseLessons",
                newName: "LessonDescription");

            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "CourseLessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionType",
                table: "CourseLessons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
