using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 作业信息
    /// </summary>
    public class Assignment
    {
        [Key]
        public Guid AssignmentId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Deadline { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid? TeacherId { get; set; }= Guid.Empty;
        public User? Teacher { get; set; }


        [ForeignKey(nameof(FileSet))]
        public Guid? FileSetId { get; set; }
        public FileSet? FileSet { get; set; }

        public List<AssignmentDivision> AssignmentDivisions { get; set; }
        public List<Submission> Submissions { get; set; }

        public bool IsHiding { get; set; }=false;
        public bool AutoRename{ get; set; }=true;

        public double FileSizeLimit { get; set; } = 50*1024*1024;
        public FileType FileTypeLimit { get; set; } = FileType.无;

    }

    public enum FileType
    {
        无,
        图片,
        视频,
        音频,
        文档,
        压缩文件,
    }
}
