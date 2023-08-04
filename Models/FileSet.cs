using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class FileSet
    {
        [Key]
        public Guid FileSetId { get; set; }

        public FilePurposeType FilePurposeType { get; set; }
        public Guid? PurposeId { get; set; }// maybe delete?

        [ForeignKey(nameof(UploadUser))]
        public Guid UploadUserId { get; set; }
        public User UploadUser { get; set; }

        public List<FileData> Files { get; set; }
    }

    public enum FilePurposeType
    {
        其它,
        作业发布,
        作业提交,
        公告发布,        
    }
}
