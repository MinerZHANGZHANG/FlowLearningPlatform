using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
    public class AddAssignmentDivision
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool HaveFile { get; set; } = true;
        public double MaxFileSize{ get; set; } = 50f;
        public SubmissionType SubmissionType { get; set; }
    }
}
