using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 课程信息
    /// </summary>
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(DepartmentType))]
        public Guid DepartmentTypeId { get; set; }
        public DepartmentType DepartmentType { get; set; }

        public List<Assignment> Assignments { get; set; }
        public List<User> Users { get; set; } // 这个课程的相关用户 包括学生和老师
    }
}
