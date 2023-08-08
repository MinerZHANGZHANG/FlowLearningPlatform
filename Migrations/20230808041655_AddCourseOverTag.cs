using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseOverTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOver",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOver",
                table: "Courses");
        }
    }
}
