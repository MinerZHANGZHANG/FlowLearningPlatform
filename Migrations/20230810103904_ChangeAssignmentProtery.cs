using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAssignmentProtery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentDivisions_SubmissionTypes_SubmissionTypeId",
                table: "AssignmentDivisions");

            migrationBuilder.DropTable(
                name: "SubmissionTypes");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentDivisions_SubmissionTypeId",
                table: "AssignmentDivisions");

            migrationBuilder.DropColumn(
                name: "SubmissionTypeId",
                table: "AssignmentDivisions");

            migrationBuilder.AddColumn<double>(
                name: "FileSizeLimit",
                table: "Assignments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FileTypeLimit",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileSizeLimit",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "FileTypeLimit",
                table: "Assignments");

            migrationBuilder.AddColumn<Guid>(
                name: "SubmissionTypeId",
                table: "AssignmentDivisions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubmissionTypes",
                columns: table => new
                {
                    SubmissionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionTypes", x => x.SubmissionTypeId);
                });

            migrationBuilder.InsertData(
                table: "SubmissionTypes",
                columns: new[] { "SubmissionTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("15352bb8-e5e3-4152-bfd9-1c72b4db7b45"), "音频" },
                    { new Guid("2051b2b2-80e1-4e8c-b8c9-6c20a9ac214b"), "压缩包" },
                    { new Guid("3cc124a8-4fe3-4810-b8cf-3ddc833dace3"), "文档" },
                    { new Guid("61177543-dbd7-4789-b4ae-da4cb737e60a"), "未确定" },
                    { new Guid("7c95e7af-7fdc-4755-91e0-e679731cea23"), "富文本" },
                    { new Guid("7d8f51a2-0ce2-43a0-a4c0-cb536a39358b"), "视频" },
                    { new Guid("a5e0fc18-d5fc-45c3-b210-0cb6a1a9fb5a"), "图片" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentDivisions_SubmissionTypeId",
                table: "AssignmentDivisions",
                column: "SubmissionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentDivisions_SubmissionTypes_SubmissionTypeId",
                table: "AssignmentDivisions",
                column: "SubmissionTypeId",
                principalTable: "SubmissionTypes",
                principalColumn: "SubmissionTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
