using FlowLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowLearningPlatform.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentDivision> AssignmentDivisions { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<FileSet> FileSets { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<User> Users { get; set; }


        public DbSet<DepartmentType> DepartmentTypes { get; set; }
        public DbSet<FilePurposeType> FilePurposeTypes { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<SchoolType> SchoolTypes { get; set; }
        public DbSet<SubmissionType> SubmissionTypes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>()
               .HasOne(a => a.FileSet).WithMany()
               .HasForeignKey(f => f.FileSetId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
               .HasOne(s => s.FileSet).WithMany()
               .HasForeignKey(f => f.FileSetId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student).WithMany()
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(s => s.DepartmentType).WithMany()
                .HasForeignKey(f => f.DepartmentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FilePurposeType>().HasData(
                new FilePurposeType() { FilePurposeTypeId = Guid.NewGuid(), Name = "作业发布" },
                new FilePurposeType() { FilePurposeTypeId = Guid.NewGuid(), Name = "作业提交" },
                new FilePurposeType() { FilePurposeTypeId = Guid.NewGuid(), Name = "公告发布" }
            );
            modelBuilder.Entity<RoleType>().HasData(
                new RoleType() { RoleTypeId = Guid.NewGuid(), Name = "未知" },
                new RoleType() { RoleTypeId = Guid.NewGuid(), Name = "教师" },
                new RoleType() { RoleTypeId = Guid.NewGuid(), Name = "管理员" },
                new RoleType() { RoleTypeId = Guid.NewGuid(), Name = "学生" }
            );


            Guid guids = Guid.NewGuid(), guidi = Guid.NewGuid(), guidu = Guid.NewGuid();
            modelBuilder.Entity<SchoolType>().HasData(
                new SchoolType() { SchoolTypeId = guidu, Name = "未确定" },
                new SchoolType() { SchoolTypeId = guids, Name = "软件学院" },
                new SchoolType() { SchoolTypeId = guidi, Name = "信息学院" }
            );

            modelBuilder.Entity<DepartmentType>().HasData(
                new DepartmentType() { DepartmentTypeId = Guid.NewGuid(), Name = "未确定",SchoolId= guidu },
                new DepartmentType() { DepartmentTypeId = Guid.NewGuid(), Name = "软件工程",SchoolId=guids},
                new DepartmentType() { DepartmentTypeId = Guid.NewGuid(), Name = "网络空间安全", SchoolId = guids },
                new DepartmentType() { DepartmentTypeId = Guid.NewGuid(), Name = "数字媒体技术", SchoolId = guids },
                new DepartmentType() { DepartmentTypeId = Guid.NewGuid(), Name = "计算机科学与技术", SchoolId = guidi },
                new DepartmentType() { DepartmentTypeId = Guid.NewGuid(), Name = "通信工程", SchoolId = guidi }
            );

            modelBuilder.Entity<SubmissionType>().HasData(
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "未确定" },
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "富文本" },
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "文档" },
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "图片" },
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "视频" },
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "音频" },
                new SubmissionType() { SubmissionTypeId = Guid.NewGuid(), Name = "压缩包" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
