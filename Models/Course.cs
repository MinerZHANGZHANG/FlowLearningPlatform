using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(Teacher))]
        public Guid TeacherId { get; set; }
        public User Teacher { get; set; }

        [ForeignKey(nameof(DepartmentType))]
        public Guid DepartmentTypeId { get; set; }
        public DepartmentType DepartmentType { get; set; }

        public List<Assignment> Assignments { get; set; }
    }
}
