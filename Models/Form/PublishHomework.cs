using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowLearningPlatform.Models.Form
{
    /// <summary>
    /// 发布作业的表单
    /// </summary>
    public class PublishHomework
    {
        [Required(ErrorMessage = "作业标题不能为空")]
        public string Title { get; set; }
        [StringLength(1023,ErrorMessage = "作业描述过长")]
        public string Description { get; set; }=string.Empty;
        [Required(ErrorMessage = "课程号不能为空")]
        public Guid CourseId { get; set; }
        [Required(ErrorMessage = "作业截止时间不能为空")]
        public DateTime[] TimeRange { get; set; } = new DateTime[] { DateTime.Now, DateTime.Now.AddDays(10) };

        public bool AutoRename { get; set; }
        public Guid? FileSetId { get; set; }

        //public List<AssignmentDivision> AssignmentDivisions { get; set; }
        public double FileSizeLimit { get; set; } = 50 * 1024 * 1024;
        public FileType FileTypeLimit { get; set; } = FileType.无;


        [JsonIgnore]
        public DateTime StartTime { get { return TimeRange[0]; }set { TimeRange[0] = value; } }
        [JsonIgnore]
        [Required]
        public DateTime DeadLine { get { return TimeRange[1]; }set { TimeRange[1] = value; } }
    }
}
