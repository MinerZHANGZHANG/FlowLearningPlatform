using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 多个FileData组成的文件集合
    /// </summary>
    public class FileSet
    {
        [Key]
        public Guid FileSetId { get; set; }
        public FilePurposeType FilePurposeType { get; set; }

        [ForeignKey(nameof(UploadUser))]
        public Guid UploadUserId { get; set; }
        public User UploadUser { get; set; }

        public List<FileData> Files { get; set; }
    }

    /// <summary>
    /// 文件的使用类型
    /// </summary>
    public enum FilePurposeType
    {
        其它,
        作业发布,
        作业提交,
        公告发布,        
    }
}
