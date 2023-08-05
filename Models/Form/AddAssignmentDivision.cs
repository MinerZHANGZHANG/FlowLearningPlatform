using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
    public class AddAssignmentDivision
    {
        [Required(ErrorMessage ="标题不能为空")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HaveFile { get; set; } = true;
        [Range(0,100,ErrorMessage ="文件大小不能超过100MB")]
        public double MaxFileMBSize{ get; set; } = 50f;
        public Guid SubmissionTypeId { get; set; }
    }
}
