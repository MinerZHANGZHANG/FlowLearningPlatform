using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 维护用户和课程的多对多关系
    /// </summary>
    public class UserCourse
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        [Column(TypeName = "decimal(6, 3)")]
        public decimal Score { get; set; }
    }
}
