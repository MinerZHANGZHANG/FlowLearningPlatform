using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
    /// <summary>
    /// 作业提交信息
    /// </summary>
    public class SubmissonInfo
    {
        public DateTime SubmissionTime { get; set; }
        public Guid AssignmentId { get; set; }
        public Guid StudentId { get; set; }
        public string RichText { get; set; } = string.Empty;     
        public Guid? FileSetId { get; set; }
    }
}
