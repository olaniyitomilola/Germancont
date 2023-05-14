using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace German.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dropUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
             name: "users",
             schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
