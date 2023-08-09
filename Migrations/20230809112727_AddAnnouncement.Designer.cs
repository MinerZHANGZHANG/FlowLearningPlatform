﻿// <auto-generated />
using System;
using FlowLearningPlatform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlowLearningPlatform.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230809112727_AddAnnouncement")]
    partial class AddAnnouncement
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlowLearningPlatform.Models.Announcement", b =>
                {
                    b.Property<Guid>("AnnouncementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("HtmlText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(0);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnnouncementId");

                    b.HasIndex("UserId");

                    b.ToTable("Announcements");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Assignment", b =>
                {
                    b.Property<Guid>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AutoRename")
                        .HasColumnType("bit");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FileSetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsHiding")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("AssignmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("FileSetId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.AssignmentDivision", b =>
                {
                    b.Property<Guid>("AssignmentDivisionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("FileSizeLimite")
                        .HasColumnType("real");

                    b.Property<Guid>("SubmissionTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssignmentDivisionId");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("SubmissionTypeId");

                    b.ToTable("AssignmentDivisions");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CourseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DepartmentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOver")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentTypeId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Enum.DepartmentType", b =>
                {
                    b.Property<Guid>("DepartmentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SchoolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DepartmentTypeId");

                    b.HasIndex("SchoolId");

                    b.ToTable("DepartmentTypes");

                    b.HasData(
                        new
                        {
                            DepartmentTypeId = new Guid("129314d7-5818-4291-9812-a8815df302c4"),
                            Name = "未确定",
                            SchoolId = new Guid("eff35450-e2c4-4f2a-8121-76e0305693ed")
                        },
                        new
                        {
                            DepartmentTypeId = new Guid("56dcfcd1-dd45-4ad0-86a1-1e54fb8c3a6a"),
                            Name = "软件工程",
                            SchoolId = new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a")
                        },
                        new
                        {
                            DepartmentTypeId = new Guid("af5a354c-1abd-41a7-a2b5-c5264cd6d7ed"),
                            Name = "网络空间安全",
                            SchoolId = new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a")
                        },
                        new
                        {
                            DepartmentTypeId = new Guid("302b94d1-e254-413b-9a7a-3e44fb837419"),
                            Name = "数字媒体技术",
                            SchoolId = new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a")
                        },
                        new
                        {
                            DepartmentTypeId = new Guid("222454b7-fc4b-4102-8b31-fd23bc89ab8e"),
                            Name = "计算机科学与技术",
                            SchoolId = new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b")
                        },
                        new
                        {
                            DepartmentTypeId = new Guid("f7773807-01a5-4825-8a11-f4c0b44a9f2c"),
                            Name = "通信工程",
                            SchoolId = new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b")
                        });
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Enum.RoleType", b =>
                {
                    b.Property<Guid>("RoleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleTypeId");

                    b.ToTable("RoleTypes");

                    b.HasData(
                        new
                        {
                            RoleTypeId = new Guid("346164e4-7e3d-4b17-bef0-c380445c03b7"),
                            Name = "未知"
                        },
                        new
                        {
                            RoleTypeId = new Guid("9882cd39-3318-4001-913d-9d6f67a0458c"),
                            Name = "教师"
                        },
                        new
                        {
                            RoleTypeId = new Guid("bea1b9b9-8b7c-4b90-9a18-fa544456abb0"),
                            Name = "管理员"
                        },
                        new
                        {
                            RoleTypeId = new Guid("19ffb7d1-3a7f-4bdf-a02f-0b9d36cc35a6"),
                            Name = "学生"
                        });
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Enum.SchoolType", b =>
                {
                    b.Property<Guid>("SchoolTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolTypeId");

                    b.ToTable("SchoolTypes");

                    b.HasData(
                        new
                        {
                            SchoolTypeId = new Guid("eff35450-e2c4-4f2a-8121-76e0305693ed"),
                            Name = "未确定"
                        },
                        new
                        {
                            SchoolTypeId = new Guid("d7105c30-624f-455a-9967-0b9fc7b1e18a"),
                            Name = "软件学院"
                        },
                        new
                        {
                            SchoolTypeId = new Guid("efd88a76-8f8b-4b00-90e2-3b4d74e1f71b"),
                            Name = "信息学院"
                        });
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Enum.SubmissionType", b =>
                {
                    b.Property<Guid>("SubmissionTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubmissionTypeId");

                    b.ToTable("SubmissionTypes");

                    b.HasData(
                        new
                        {
                            SubmissionTypeId = new Guid("61177543-dbd7-4789-b4ae-da4cb737e60a"),
                            Name = "未确定"
                        },
                        new
                        {
                            SubmissionTypeId = new Guid("7c95e7af-7fdc-4755-91e0-e679731cea23"),
                            Name = "富文本"
                        },
                        new
                        {
                            SubmissionTypeId = new Guid("3cc124a8-4fe3-4810-b8cf-3ddc833dace3"),
                            Name = "文档"
                        },
                        new
                        {
                            SubmissionTypeId = new Guid("a5e0fc18-d5fc-45c3-b210-0cb6a1a9fb5a"),
                            Name = "图片"
                        },
                        new
                        {
                            SubmissionTypeId = new Guid("7d8f51a2-0ce2-43a0-a4c0-cb536a39358b"),
                            Name = "视频"
                        },
                        new
                        {
                            SubmissionTypeId = new Guid("15352bb8-e5e3-4152-bfd9-1c72b4db7b45"),
                            Name = "音频"
                        },
                        new
                        {
                            SubmissionTypeId = new Guid("2051b2b2-80e1-4e8c-b8c9-6c20a9ac214b"),
                            Name = "压缩包"
                        });
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.FileData", b =>
                {
                    b.Property<Guid>("FileDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FileSetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Md5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<int>("StorageType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("datetime2");

                    b.HasKey("FileDataId");

                    b.HasIndex("FileSetId");

                    b.ToTable("FileDatas");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.FileSet", b =>
                {
                    b.Property<Guid>("FileSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("FilePurposeType")
                        .HasColumnType("int");

                    b.Property<Guid>("UploadUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FileSetId");

                    b.HasIndex("UploadUserId");

                    b.ToTable("FileSets");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Submission", b =>
                {
                    b.Property<Guid>("SubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("FileSetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RichText")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(6, 3)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SubmissionCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmissionTime")
                        .HasColumnType("datetime2");

                    b.HasKey("SubmissionId");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("FileSetId");

                    b.HasIndex("StudentId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Brithday")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DepartmentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RoleTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StudentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("DepartmentTypeId");

                    b.HasIndex("RoleTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.UserCourse", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Score")
                        .HasColumnType("decimal(6, 3)");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Announcement", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Assignment", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Course", "Course")
                        .WithMany("Assignments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowLearningPlatform.Models.FileSet", "FileSet")
                        .WithMany()
                        .HasForeignKey("FileSetId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Course");

                    b.Navigation("FileSet");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.AssignmentDivision", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Assignment", "Assignment")
                        .WithMany("AssignmentDivisions")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowLearningPlatform.Models.Enum.SubmissionType", "SubmissionType")
                        .WithMany()
                        .HasForeignKey("SubmissionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("SubmissionType");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Course", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Enum.DepartmentType", "DepartmentType")
                        .WithMany()
                        .HasForeignKey("DepartmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartmentType");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Enum.DepartmentType", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Enum.SchoolType", "SchoolType")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SchoolType");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.FileData", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.FileSet", "FileSet")
                        .WithMany("Files")
                        .HasForeignKey("FileSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileSet");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.FileSet", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.User", "UploadUser")
                        .WithMany()
                        .HasForeignKey("UploadUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadUser");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Submission", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowLearningPlatform.Models.FileSet", "FileSet")
                        .WithMany()
                        .HasForeignKey("FileSetId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FlowLearningPlatform.Models.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("FileSet");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.User", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Enum.DepartmentType", "DepartmentType")
                        .WithMany()
                        .HasForeignKey("DepartmentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FlowLearningPlatform.Models.Enum.RoleType", "RoleType")
                        .WithMany()
                        .HasForeignKey("RoleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartmentType");

                    b.Navigation("RoleType");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.UserCourse", b =>
                {
                    b.HasOne("FlowLearningPlatform.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlowLearningPlatform.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Assignment", b =>
                {
                    b.Navigation("AssignmentDivisions");

                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.Course", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("FlowLearningPlatform.Models.FileSet", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
