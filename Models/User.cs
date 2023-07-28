using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [ForeignKey(nameof(RoleType))]
        public Guid RoleTypeId { get; set; }
        public RoleType? RoleType { get; set; }

        [ForeignKey(nameof(DepartmentType))]
        public Guid DepartmentTypeId { get; set; }
        public DepartmentType? DepartmentType { get; set; }
      
        public DateTime RegTime { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public DateTime? Brithday { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
