using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubmissionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGrade",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGrade",
                table: "Submissions");
        }
    }
}
