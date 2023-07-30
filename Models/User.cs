using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text.Json.Serialization;

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

        public List<Course> Courses { get; set; }

        public ClaimsPrincipal ToClaimsPrincipal() =>
            new(new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name,Name),
                //new(ClaimTypes.Hash,PasswordHash.ToString()),
                new(ClaimTypes.Role,RoleType.RoleTypeId.ToString()),
            }, "jwt"));

        public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
        {
            Name = principal.FindFirstValue(ClaimTypes.Name),
            //PasswordHash = principal.FindFirstValue(ClaimTypes.Hash)
            RoleTypeId =Guid.Parse(principal.FindFirstValue(ClaimTypes.Role)),
        };
    }
}
