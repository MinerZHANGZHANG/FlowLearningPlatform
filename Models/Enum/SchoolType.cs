using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Enum
{
    public class SchoolType
    {
        [Key]
        public Guid SchoolTypeId { get; set; }
        public string Name { get; set; }

    }


}
