using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Assignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Partworks");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Brithday",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentTypeId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleTypeId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FilePurposeTypes",
                columns: table => new
                {
                    FilePurposeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilePurposeTypes", x => x.FilePurposeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RoleTypes",
                columns: table => new
                {
                    RoleTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTypes", x => x.RoleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolTypes",
                columns: table => new
                {
                    SchoolTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolTypes", x => x.SchoolTypeId);
                });

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

            migrationBuilder.CreateTable(
                name: "FileSets",
                columns: table => new
                {
                    FileSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePurposeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurposeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploadUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSets", x => x.FileSetId);
                    table.ForeignKey(
                        name: "FK_FileSets_FilePurposeTypes_FilePurposeTypeId",
                        column: x => x.FilePurposeTypeId,
                        principalTable: "FilePurposeTypes",
                        principalColumn: "FilePurposeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileSets_Users_UploadUserId",
                        column: x => x.UploadUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentTypes",
                columns: table => new
                {
                    DepartmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentTypes", x => x.DepartmentTypeId);
                    table.ForeignKey(
                        name: "FK_DepartmentTypes_SchoolTypes_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "SchoolTypes",
                        principalColumn: "SchoolTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileDatas",
                columns: table => new
                {
                    FileDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Suffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageType = table.Column<int>(type: "int", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDatas", x => x.FileDataId);
                    table.ForeignKey(
                        name: "FK_FileDatas_FileSets_FileSetId",
                        column: x => x.FileSetId,
                        principalTable: "FileSets",
                        principalColumn: "FileSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_DepartmentTypes_DepartmentTypeId",
                        column: x => x.DepartmentTypeId,
                        principalTable: "DepartmentTypes",
                        principalColumn: "DepartmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_FileSets_FileSetId",
                        column: x => x.FileSetId,
                        principalTable: "FileSets",
                        principalColumn: "FileSetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentDivisions",
                columns: table => new
                {
                    AssignmentDivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileSizeLimite = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentDivisions", x => x.AssignmentDivisionId);
                    table.ForeignKey(
                        name: "FK_AssignmentDivisions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentDivisions_SubmissionTypes_SubmissionTypeId",
                        column: x => x.SubmissionTypeId,
                        principalTable: "SubmissionTypes",
                        principalColumn: "SubmissionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmissionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmissionCount = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    Assignmentd = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_Assignmentd",
                        column: x => x.Assignmentd,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_FileSets_FileSetId",
                        column: x => x.FileSetId,
                        principalTable: "FileSets",
                        principalColumn: "FileSetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submissions_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Users_DepartmentTypeId",
                table: "Users",
                column: "DepartmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleTypeId",
                table: "Users",
                column: "RoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentDivisions_AssignmentId",
                table: "AssignmentDivisions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentDivisions_SubmissionTypeId",
                table: "AssignmentDivisions",
                column: "SubmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_FileSetId",
                table: "Assignments",
                column: "FileSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentTypeId",
                table: "Courses",
                column: "DepartmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentTypes_SchoolId",
                table: "DepartmentTypes",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_FileDatas_FileSetId",
                table: "FileDatas",
                column: "FileSetId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSets_FilePurposeTypeId",
                table: "FileSets",
                column: "FilePurposeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileSets_UploadUserId",
                table: "FileSets",
                column: "UploadUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_Assignmentd",
                table: "Submissions",
                column: "Assignmentd");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_FileSetId",
                table: "Submissions",
                column: "FileSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentId",
                table: "Submissions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DepartmentTypes_DepartmentTypeId",
                table: "Users",
                column: "DepartmentTypeId",
                principalTable: "DepartmentTypes",
                principalColumn: "DepartmentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RoleTypes_RoleTypeId",
                table: "Users",
                column: "RoleTypeId",
                principalTable: "RoleTypes",
                principalColumn: "RoleTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DepartmentTypes_DepartmentTypeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RoleTypes_RoleTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AssignmentDivisions");

            migrationBuilder.DropTable(
                name: "FileDatas");

            migrationBuilder.DropTable(
                name: "RoleTypes");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "SubmissionTypes");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "FileSets");

            migrationBuilder.DropTable(
                name: "DepartmentTypes");

            migrationBuilder.DropTable(
                name: "FilePurposeTypes");

            migrationBuilder.DropTable(
                name: "SchoolTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Brithday",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleTypeId",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Major",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.BoardId);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    HomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.HomeworkId);
                });

            migrationBuilder.CreateTable(
                name: "Partworks",
                columns: table => new
                {
                    PartworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partworks", x => x.PartworkId);
                });
        }
    }
}
