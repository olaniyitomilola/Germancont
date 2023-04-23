using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace German.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuthorCourseLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorCourseLessons",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CourseLessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorCourseLessons", x => new { x.AuthorId, x.CourseLessonId });
                    table.ForeignKey(
                        name: "FK_AuthorCourseLessons_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorCourseLessons_CourseLessons_CourseLessonId",
                        column: x => x.CourseLessonId,
                        principalTable: "CourseLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorCourseLessons_CourseLessonId",
                table: "AuthorCourseLessons",
                column: "CourseLessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorCourseLessons");
        }
    }
}
