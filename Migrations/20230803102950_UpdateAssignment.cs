using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<Guid>(
                name: "FileSetId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<bool>(
                name: "AutoRename",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHiding",
                table: "Assignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "FilePurposeTypes",
                columns: new[] { "FilePurposeTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("46c5d65e-3088-480a-a417-240873f2b62b"), "作业发布" },
                    { new Guid("494c1403-cf4e-487a-8a6f-d0c5dc82e239"), "公告发布" },
                    { new Guid("e9a019a2-57a6-447d-b568-b9e29e6cc2fa"), "作业提交" }
                });

            migrationBuilder.InsertData(
                table: "RoleTypes",
                columns: new[] { "RoleTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("19ffb7d1-3a7f-4bdf-a02f-0b9d36cc35a6"), "学生" },
                    { new Guid("346164e4-7e3d-4b17-bef0-c380445c03b7"), "未知" },
                    { new Guid("9882cd39-3318-4001-913d-9d6f67a0458c"), "教师" },
                    { new Guid("bea1b9b9-8b7c-4b90-9a18-fa544456abb0"), "管理员" }
                });

            migrationBuilder.InsertData(
                table: "SchoolTypes",
                columns: new[] { "SchoolTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a"), "软件学院" },
                    { new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b"), "信息学院" },
                    { new Guid("eff35450-e2c4-4f2a-8121-76e0305693ed"), "未确定" }
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

            migrationBuilder.InsertData(
                table: "DepartmentTypes",
                columns: new[] { "DepartmentTypeId", "Name", "SchoolId" },
                values: new object[,]
                {
                    { new Guid("129314d7-5818-4291-9812-a8815df302c4"), "未确定", new Guid("eff35450-e2c4-4f2a-8121-76e0305693ed") },
                    { new Guid("222454b7-fc4b-4102-8b31-fd23bc89ab8e"), "计算机科学与技术", new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b") },
                    { new Guid("302b94d1-e254-413b-9a7a-3e44fb837419"), "数字媒体技术", new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a") },
                    { new Guid("56dcfcd1-dd45-4ad0-86a1-1e54fb8c3a6a"), "软件工程", new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a") },
                    { new Guid("af5a354c-1abd-41a7-a2b5-c5264cd6d7ed"), "网络空间安全", new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a") },
                    { new Guid("f7773807-01a5-4825-8a11-f4c0b44a9f2c"), "通信工程", new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("129314d7-5818-4291-9812-a8815df302c4"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("222454b7-fc4b-4102-8b31-fd23bc89ab8e"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("302b94d1-e254-413b-9a7a-3e44fb837419"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("56dcfcd1-dd45-4ad0-86a1-1e54fb8c3a6a"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("af5a354c-1abd-41a7-a2b5-c5264cd6d7ed"));

            migrationBuilder.DeleteData(
                table: "DepartmentTypes",
                keyColumn: "DepartmentTypeId",
                keyValue: new Guid("f7773807-01a5-4825-8a11-f4c0b44a9f2c"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("46c5d65e-3088-480a-a417-240873f2b62b"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("494c1403-cf4e-487a-8a6f-d0c5dc82e239"));

            migrationBuilder.DeleteData(
                table: "FilePurposeTypes",
                keyColumn: "FilePurposeTypeId",
                keyValue: new Guid("e9a019a2-57a6-447d-b568-b9e29e6cc2fa"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("19ffb7d1-3a7f-4bdf-a02f-0b9d36cc35a6"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("346164e4-7e3d-4b17-bef0-c380445c03b7"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("9882cd39-3318-4001-913d-9d6f67a0458c"));

            migrationBuilder.DeleteData(
                table: "RoleTypes",
                keyColumn: "RoleTypeId",
                keyValue: new Guid("bea1b9b9-8b7c-4b90-9a18-fa544456abb0"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("15352bb8-e5e3-4152-bfd9-1c72b4db7b45"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("2051b2b2-80e1-4e8c-b8c9-6c20a9ac214b"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("3cc124a8-4fe3-4810-b8cf-3ddc833dace3"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("61177543-dbd7-4789-b4ae-da4cb737e60a"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("7c95e7af-7fdc-4755-91e0-e679731cea23"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("7d8f51a2-0ce2-43a0-a4c0-cb536a39358b"));

            migrationBuilder.DeleteData(
                table: "SubmissionTypes",
                keyColumn: "SubmissionTypeId",
                keyValue: new Guid("a5e0fc18-d5fc-45c3-b210-0cb6a1a9fb5a"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b"));

            migrationBuilder.DeleteData(
                table: "SchoolTypes",
                keyColumn: "SchoolTypeId",
                keyValue: new Guid("eff35450-e2c4-4f2a-8121-76e0305693ed"));

            migrationBuilder.DropColumn(
                name: "AutoRename",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "IsHiding",
                table: "Assignments");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileSetId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
        }
    }
}
