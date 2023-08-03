using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class Assignment
    {
        [Key]
        public Guid AssignmentId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateTime { get; set; }//StartTime
        public DateTime Deadline { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey(nameof(FileSet))]
        public Guid? FileSetId { get; set; }
        public FileSet? FileSet { get; set; }

        public List<AssignmentDivision> AssignmentDivisions { get; set; }
        public List<Submission> Submissions { get; set; }

        public bool IsHiding { get; set; }=false;
        public bool AutoRename{ get; set; }=true;
    }
}
