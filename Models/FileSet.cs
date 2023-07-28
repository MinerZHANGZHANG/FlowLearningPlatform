using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class FileSet
    {
        [Key]
        public Guid FileSetId { get; set; }

        [ForeignKey(nameof(FilePurposeType))]
        public Guid FilePurposeTypeId { get; set; }
        public FilePurposeType FilePurposeType { get; set; }
        public Guid PurposeId { get; set; }

        [ForeignKey(nameof(UploadUser))]
        public Guid UploadUserId { get; set; }
        public User UploadUser { get; set; }

        public List<FileData> Files { get; set; }
    }

}
