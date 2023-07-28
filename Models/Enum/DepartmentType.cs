using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models.Enum
{
    public class DepartmentType
    {
        [Key]
        public Guid DepartmentTypeId { get; set; }
        public string Name { get; set;}

        [ForeignKey(nameof(SchoolType))]
        public Guid SchoolId { get; set;}
        public SchoolType? SchoolType { get; set;}
    }


}
