using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Enum
{
    /// <summary>
    /// 提交作业的类型
    /// </summary>
    public class SubmissionType
    {
        [Key]
        public Guid SubmissionTypeId { get; set; }
        public string Name { get; set;}
    }

}
