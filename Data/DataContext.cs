using FlowLearningPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowLearningPlatform.Data
{
    public class DataContext : DbContext
    {
        public virtual  DbSet<Assignment> Assignments { get; set; }
        public virtual  DbSet<AssignmentDivision> AssignmentDivisions { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<FileData> FileDatas { get; set; }
        public virtual DbSet<FileSet> FileSets { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }
        public virtual DbSet<User> Users { get; set; }


        public virtual DbSet<DepartmentType> DepartmentTypes { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<SchoolType> SchoolTypes { get; set; }
        public virtual DbSet<SubmissionType> SubmissionTypes { get; set; }

        public virtual DbSet<UserCourse> UserCourses { get; set; }  

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

		public DataContext()
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

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Courses)
                .UsingEntity<UserCourse>();

            //modelBuilder.Entity<FilePurposeType>().HasData(
            //    new FilePurposeType() { FilePurposeTypeId =Guid.Parse("46C5D65E-3088-480A-A417-240873F2B62B"), Name = "作业发布" },
            //    new FilePurposeType() { FilePurposeTypeId = Guid.Parse("E9A019A2-57A6-447D-B568-B9E29E6CC2FA"), Name = "作业提交" },
            //    new FilePurposeType() { FilePurposeTypeId = Guid.Parse("494C1403-CF4E-487A-8A6F-D0C5DC82E239"), Name = "公告发布" }
            //);
            modelBuilder.Entity<RoleType>().HasData(
                new RoleType() { RoleTypeId = Guid.Parse("346164E4-7E3D-4B17-BEF0-C380445C03B7"), Name = "未知" },
                new RoleType() { RoleTypeId = Guid.Parse("9882CD39-3318-4001-913D-9D6F67A0458C"), Name = "教师" },
                new RoleType() { RoleTypeId = Guid.Parse("BEA1B9B9-8B7C-4B90-9A18-FA544456ABB0"), Name = "管理员" },
                new RoleType() { RoleTypeId = Guid.Parse("19FFB7D1-3A7F-4BDF-A02F-0B9D36CC35A6"), Name = "学生" }
            );


            Guid guids = Guid.Parse("D7105C30-624F-455A-9967-0B9FC7B1E18A"),
                guidi = Guid.Parse("EFD88A76-8F8B-4B00-90E2-3B4D74E1F71B"), 
                guidu = Guid.Parse("EFF35450-E2C4-4F2A-8121-76E0305693ED");
            modelBuilder.Entity<SchoolType>().HasData(
                new SchoolType() { SchoolTypeId = guidu, Name = "未确定" },
                new SchoolType() { SchoolTypeId = guids, Name = "软件学院" },
                new SchoolType() { SchoolTypeId = guidi, Name = "信息学院" }
            );

            modelBuilder.Entity<DepartmentType>().HasData(
                new DepartmentType() { DepartmentTypeId = Guid.Parse("129314D7-5818-4291-9812-A8815DF302C4"), Name = "未确定",SchoolId= guidu },
                new DepartmentType() { DepartmentTypeId = Guid.Parse("56DCFCD1-DD45-4AD0-86A1-1E54FB8C3A6A"), Name = "软件工程",SchoolId=guids},
                new DepartmentType() { DepartmentTypeId = Guid.Parse("AF5A354C-1ABD-41A7-A2B5-C5264CD6D7ED"), Name = "网络空间安全", SchoolId = guids },
                new DepartmentType() { DepartmentTypeId = Guid.Parse("302B94D1-E254-413B-9A7A-3E44FB837419"), Name = "数字媒体技术", SchoolId = guids },
                new DepartmentType() { DepartmentTypeId = Guid.Parse("222454B7-FC4B-4102-8B31-FD23BC89AB8E"), Name = "计算机科学与技术", SchoolId = guidi },
                new DepartmentType() { DepartmentTypeId = Guid.Parse("F7773807-01A5-4825-8A11-F4C0B44A9F2C"), Name = "通信工程", SchoolId = guidi }
            );

            modelBuilder.Entity<SubmissionType>().HasData(
                new SubmissionType() { SubmissionTypeId = Guid.Parse("61177543-DBD7-4789-B4AE-DA4CB737E60A"), Name = "未确定" },
                new SubmissionType() { SubmissionTypeId = Guid.Parse("7C95E7AF-7FDC-4755-91E0-E679731CEA23"), Name = "富文本" },
                new SubmissionType() { SubmissionTypeId = Guid.Parse("3CC124A8-4FE3-4810-B8CF-3DDC833DACE3"), Name = "文档" },
                new SubmissionType() { SubmissionTypeId = Guid.Parse("A5E0FC18-D5FC-45C3-B210-0CB6A1A9FB5A"), Name = "图片" },
                new SubmissionType() { SubmissionTypeId = Guid.Parse("7D8F51A2-0CE2-43A0-A4C0-CB536A39358B"), Name = "视频" },
                new SubmissionType() { SubmissionTypeId = Guid.Parse("15352BB8-E5E3-4152-BFD9-1C72B4DB7B45"), Name = "音频" },
                new SubmissionType() { SubmissionTypeId = Guid.Parse("2051B2B2-80E1-4E8C-B8C9-6C20A9AC214B"), Name = "压缩包" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
