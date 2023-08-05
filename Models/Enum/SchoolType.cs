using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Enum
{
    /// <summary>
    /// 学院类型
    /// </summary>
    public class SchoolType
    {
        [Key]
        public Guid SchoolTypeId { get; set; }
        public string Name { get; set; }
    }
}
