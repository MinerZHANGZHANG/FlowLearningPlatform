using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class addCourseUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("418ac08b-0dba-4c4a-99a5-fa3d17f139f4"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("68ec7ddc-68e7-4893-8831-f895ff75d402"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("a0845bb0-5850-4cad-9834-3ddf93e678be"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("a4bbc6ef-d2fa-4ef7-89ef-8b3b2f0e260b"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("aab07032-f995-4773-964a-c4cc85ad09a5"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("c4f7137f-daf4-45c4-bc0c-a3c995968e94"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("37dc7e0e-5972-4454-af19-09de3dc26ac6"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("c30fc01a-46d2-4077-bbdb-6f4caa4f0f8c"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("ceb29c89-4049-4396-8483-01e02f96b853"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("7611f5d0-b6b5-4c46-8ece-f1a0d2d71f2d"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("aef75f95-8138-4b47-bdd0-45cc47da4567"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("bf57bdb1-9095-429a-a743-a74785e999e9"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("cdc7fe6b-eaa4-4cee-b7de-2dbc048b122d"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("5a709fae-93d0-4899-978d-4f271d2a43b9"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("63734e3c-e585-49da-a0dd-09ed14a223a9"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("83826beb-5e03-4cbb-9286-0e45d116f7c9"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("8bf7b898-3859-4425-bf0f-f53198c99a35"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("94a3729e-fc97-476c-9c73-87448d79a5d7"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("aeb24e81-f4e4-499c-9877-fdb4549cb1cd"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("e6ecdfed-4cc3-4325-ba02-75ca36318002"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("16015a15-9d80-45f0-b2b0-e86cb4531e1f"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("6e584649-c615-4717-be06-6664c0041f71"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("b9307e94-5e6a-4682-8f28-9c8c6d124f84"));

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "CourseNumber",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(6,3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FilePurposeTypes",
                columns: new[] { "FilePurposeTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("61444950-700c-4e4f-a1d8-3b17bd8ccff6"), "作业提交" },
                    { new Guid("87968014-2766-461b-865a-424af46b6eef"), "公告发布" },
                    { new Guid("90a3a927-db8d-4739-93f2-794c68a1a71a"), "作业发布" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "RoleTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("5a0f2626-9eef-45f9-94c8-4df1b2e9359d"), "教师" },
                    { new Guid("5caeae05-ae9c-46b6-99e6-2a16ea64ef25"), "学生" },
                    { new Guid("6bbdfb02-b79b-4b64-ad30-473fad0c0bbc"), "管理员" },
                    { new Guid("b3d10f3f-46ea-4d00-808c-2df8d9f29ebd"), "未知" }
                });

            migrationBuilder.InsertData(
                table: "SchoolTypes",
                columns: new[] { "SchoolTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("4d32191e-f958-4a51-943d-46b638174347"), "软件学院" },
                    { new Guid("c0f97b1c-6670-4e07-8210-6c4defa8c18f"), "信息学院" },
                    { new Guid("eefa2b64-fa75-4212-a303-c91dfb673a78"), "未确定" }
                });

            migrationBuilder.InsertData(
                table: "SubmissionTypes",
                columns: new[] { "SubmissionTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("3a3d25bc-279f-42ca-a18f-1306c79b07af"), "音频" },
                    { new Guid("3a800e1e-be7c-404a-b860-bbc6b82b9634"), "富文本" },
                    { new Guid("565ce254-3ff7-4b6a-a69c-922c4d58088d"), "未确定" },
                    { new Guid("5cbd28b8-67df-4f39-9d60-31523bdffdfd"), "视频" },
                    { new Guid("cfb6ec31-7f48-46b2-8497-449292cf5a97"), "压缩包" },
                    { new Guid("ea185274-44f5-4be7-bd40-df9774029fef"), "图片" },
                    { new Guid("f14a077f-5f46-4766-856e-26a13dfaf60a"), "文档" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentTypes",
                columns: new[] { "DepartmentTypeId", "Name", "SchoolId" },
                values: new object[,]
                {
                    { new Guid("3eb704fc-a2b6-4db1-be41-a80d5404866a"), "网络空间安全", new Guid("4d32191e-f958-4a51-943d-46b638174347") },
                    { new Guid("3fc0862f-c472-47ad-b42c-59ecfdf68192"), "计算机科学与技术", new Guid("c0f97b1c-6670-4e07-8210-6c4defa8c18f") },
                    { new Guid("425f4865-b6b5-4ef4-ad84-d9282f8d3d2d"), "未确定", new Guid("eefa2b64-fa75-4212-a303-c91dfb673a78") },
                    { new Guid("b34f9ab6-43f9-4408-a11f-46896f4750fa"), "数字媒体技术", new Guid("4d32191e-f958-4a51-943d-46b638174347") },
                    { new Guid("d92575b1-baf7-45c3-96d6-d8d750fcb632"), "通信工程", new Guid("c0f97b1c-6670-4e07-8210-6c4defa8c18f") },
                    { new Guid("fb96e037-afba-453a-b28d-6c9c5d387a98"), "软件工程", new Guid("4d32191e-f958-4a51-943d-46b638174347") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("3eb704fc-a2b6-4db1-be41-a80d5404866a"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("3fc0862f-c472-47ad-b42c-59ecfdf68192"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("425f4865-b6b5-4ef4-ad84-d9282f8d3d2d"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("b34f9ab6-43f9-4408-a11f-46896f4750fa"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("d92575b1-baf7-45c3-96d6-d8d750fcb632"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("fb96e037-afba-453a-b28d-6c9c5d387a98"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("61444950-700c-4e4f-a1d8-3b17bd8ccff6"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("87968014-2766-461b-865a-424af46b6eef"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("90a3a927-db8d-4739-93f2-794c68a1a71a"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("5a0f2626-9eef-45f9-94c8-4df1b2e9359d"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("5caeae05-ae9c-46b6-99e6-2a16ea64ef25"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("6bbdfb02-b79b-4b64-ad30-473fad0c0bbc"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("b3d10f3f-46ea-4d00-808c-2df8d9f29ebd"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("3a3d25bc-279f-42ca-a18f-1306c79b07af"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("3a800e1e-be7c-404a-b860-bbc6b82b9634"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("565ce254-3ff7-4b6a-a69c-922c4d58088d"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("5cbd28b8-67df-4f39-9d60-31523bdffdfd"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("cfb6ec31-7f48-46b2-8497-449292cf5a97"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("ea185274-44f5-4be7-bd40-df9774029fef"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("f14a077f-5f46-4766-856e-26a13dfaf60a"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("4d32191e-f958-4a51-943d-46b638174347"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("c0f97b1c-6670-4e07-8210-6c4defa8c18f"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("eefa2b64-fa75-4212-a303-c91dfb673a78"));

            migrationBuilder.DropColumn(
                name: "CourseNumber",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "FilePurposeTypes",
                columns: new[] { "FilePurposeTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("37dc7e0e-5972-4454-af19-09de3dc26ac6"), "公告发布" },
                    { new Guid("c30fc01a-46d2-4077-bbdb-6f4caa4f0f8c"), "作业提交" },
                    { new Guid("ceb29c89-4049-4396-8483-01e02f96b853"), "作业发布" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "RoleTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("7611f5d0-b6b5-4c46-8ece-f1a0d2d71f2d"), "管理员" },
                    { new Guid("aef75f95-8138-4b47-bdd0-45cc47da4567"), "未知" },
                    { new Guid("bf57bdb1-9095-429a-a743-a74785e999e9"), "教师" },
                    { new Guid("cdc7fe6b-eaa4-4cee-b7de-2dbc048b122d"), "学生" }
                });

            migrationBuilder.InsertData(
                table: "SchoolTypes",
                columns: new[] { "SchoolTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("16015a15-9d80-45f0-b2b0-e86cb4531e1f"), "软件学院" },
                    { new Guid("6e584649-c615-4717-be06-6664c0041f71"), "信息学院" },
                    { new Guid("b9307e94-5e6a-4682-8f28-9c8c6d124f84"), "未确定" }
                });

            migrationBuilder.InsertData(
                table: "SubmissionTypes",
                columns: new[] { "SubmissionTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("5a709fae-93d0-4899-978d-4f271d2a43b9"), "压缩包" },
                    { new Guid("63734e3c-e585-49da-a0dd-09ed14a223a9"), "文档" },
                    { new Guid("83826beb-5e03-4cbb-9286-0e45d116f7c9"), "视频" },
                    { new Guid("8bf7b898-3859-4425-bf0f-f53198c99a35"), "未确定" },
                    { new Guid("94a3729e-fc97-476c-9c73-87448d79a5d7"), "音频" },
                    { new Guid("aeb24e81-f4e4-499c-9877-fdb4549cb1cd"), "富文本" },
                    { new Guid("e6ecdfed-4cc3-4325-ba02-75ca36318002"), "图片" }
                });

            migrationBuilder.InsertData(
                table: "DepartmentTypes",
                columns: new[] { "DepartmentTypeId", "Name", "SchoolId" },
                values: new object[,]
                {
                    { new Guid("418ac08b-0dba-4c4a-99a5-fa3d17f139f4"), "计算机科学与技术", new Guid("6e584649-c615-4717-be06-6664c0041f71") },
                    { new Guid("68ec7ddc-68e7-4893-8831-f895ff75d402"), "数字媒体技术", new Guid("16015a15-9d80-45f0-b2b0-e86cb4531e1f") },
                    { new Guid("a0845bb0-5850-4cad-9834-3ddf93e678be"), "网络空间安全", new Guid("16015a15-9d80-45f0-b2b0-e86cb4531e1f") },
                    { new Guid("a4bbc6ef-d2fa-4ef7-89ef-8b3b2f0e260b"), "通信工程", new Guid("6e584649-c615-4717-be06-6664c0041f71") },
                    { new Guid("aab07032-f995-4773-964a-c4cc85ad09a5"), "软件工程", new Guid("16015a15-9d80-45f0-b2b0-e86cb4531e1f") },
                    { new Guid("c4f7137f-daf4-45c4-bc0c-a3c995968e94"), "未确定", new Guid("b9307e94-5e6a-4682-8f28-9c8c6d124f84") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
