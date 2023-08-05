using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 作业分区（待实现）
    /// </summary>
    public class AssignmentDivision
    {
        [Key]
        public Guid AssignmentDivisionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        //[ForeignKey(nameof(FileSet))]
        //public Guid FileSetId { get; set; }
        //public FileSet FileSet { get; set; }      
        public SubmissionType SubmissionType { get; set; }
        public float FileSizeLimite { get; set; }
    }


}
