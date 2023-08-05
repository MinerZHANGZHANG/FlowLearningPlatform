using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    /// <summary>
    /// 单个文件信息
    /// </summary>
    public class FileData
    {
        [Key]
        public Guid FileDataId { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public long Size { get; set; }
        public DateTime UploadTime { get; set; }

        [ForeignKey(nameof(FileSet))]
        public Guid FileSetId { get; set; }
        public FileSet FileSet { get; set; }

        public StorageType StorageType { get; set; }     
        public string? FilePath { get; set; }
        public string? Md5 { get; set; }
        
    }
    public enum StorageType
    {
        WebLink,
        LocalLink,
        Database,
    }
}
