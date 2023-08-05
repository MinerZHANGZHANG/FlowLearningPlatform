using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_Assignmentd",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "PurposeId",
                table: "FileSets");

            migrationBuilder.RenameColumn(
                name: "Assignmentd",
                table: "Submissions",
                newName: "AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_Assignmentd",
                table: "Submissions",
                newName: "IX_Submissions_AssignmentId");

            migrationBuilder.RenameColumn(
                name: "Suffix",
                table: "FileDatas",
                newName: "FileType");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "FileDatas",
                newName: "Md5");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Assignments_AssignmentId",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Submissions",
                newName: "Assignmentd");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                newName: "IX_Submissions_Assignmentd");

            migrationBuilder.RenameColumn(
                name: "Md5",
                table: "FileDatas",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "FileType",
                table: "FileDatas",
                newName: "Suffix");

            migrationBuilder.AddColumn<Guid>(
                name: "PurposeId",
                table: "FileSets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Assignments_Assignmentd",
                table: "Submissions",
                column: "Assignmentd",
                principalTable: "Assignments",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
