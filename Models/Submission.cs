using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 作业提交信息
    /// </summary>
    public class Submission
    {
        [Key]
        public Guid SubmissionId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int SubmissionCount { get; set; }

        [Column(TypeName = "decimal(6, 3)")]
        public decimal Score { get; set; }
        public bool IsGrade { get; set; }=false;

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }// rename
        public Assignment Assignment { get; set; }

        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public User Student { get; set; }

        [StringLength(10000)]
        public string RichText { get; set; }=string.Empty; // 作业提交时输入的文本

        [ForeignKey(nameof(FileSet))]
        public Guid? FileSetId { get; set; }
        public FileSet? FileSet { get; set; }
    }
}
