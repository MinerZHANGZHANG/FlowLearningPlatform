using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowLearningPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSets_FilePurposeTypes_FilePurposeTypeId",
                table: "FileSets");

            migrationBuilder.DropTable(
                name: "FilePurposeTypes");

            migrationBuilder.DropIndex(
                name: "IX_FileSets_FilePurposeTypeId",
                table: "FileSets");

            migrationBuilder.DropColumn(
                name: "FilePurposeTypeId",
                table: "FileSets");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "FileDatas");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileSetId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "RichText",
                table: "Submissions",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurposeId",
                table: "FileSets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "FilePurposeType",
                table: "FileSets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RichText",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "FilePurposeType",
                table: "FileSets");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileSetId",
                table: "Submissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurposeId",
                table: "FileSets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FilePurposeTypeId",
                table: "FileSets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "FileDatas",
                type: "varbinary(max)",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "FilePurposeTypes",
                columns: new[] { "FilePurposeTypeId", "Name" },
                values: new object[,]
                {
                    { new Guid("46c5d65e-3088-480a-a417-240873f2b62b"), "作业发布" },
                    { new Guid("494c1403-cf4e-487a-8a6f-d0c5dc82e239"), "公告发布" },
                    { new Guid("e9a019a2-57a6-447d-b568-b9e29e6cc2fa"), "作业提交" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileSets_FilePurposeTypeId",
                table: "FileSets",
                column: "FilePurposeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSets_FilePurposeTypes_FilePurposeTypeId",
                table: "FileSets",
                column: "FilePurposeTypeId",
                principalTable: "FilePurposeTypes",
                principalColumn: "FilePurposeTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
