using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowLearningPlatform.Models.Form
{
    public class PublishHomework
    {
        [Required]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }=string.Empty;
        [Required]
        public Course Course { get; set; }
        [Required]
        public DateTime[] TimeRange { get; set; } = new DateTime[] { DateTime.Now, DateTime.Now.AddDays(10) };

        public FileSet FileSet { get; set; }

        public bool AutoRename { get; set; }
        public List<AssignmentDivision> Partworks { get; set; }

        [JsonIgnore]
        public DateTime StartTime { get { return TimeRange[0]; }set { TimeRange[0] = value; } }
        [JsonIgnore]
        [Required]
        public DateTime DeadLine { get { return TimeRange[1]; }set { TimeRange[1] = value; } }
    }
}
