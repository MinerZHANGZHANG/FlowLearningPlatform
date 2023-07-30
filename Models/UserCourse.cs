using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class UserCourse
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        [Column(TypeName = "decimal(6, 3)")]
        public decimal Score { get; set; }
    }
}
