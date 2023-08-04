using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class FileData
    {
        [Key]
        public Guid FileDataId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }// delete
        public string Suffix { get; set; }// rename
        public long Size { get; set; }
        public DateTime UploadTime { get; set; }// rename

        [ForeignKey(nameof(FileSet))]
        public Guid FileSetId { get; set; }
        public FileSet FileSet { get; set; }

        public StorageType StorageType { get; set; }     
        public string? FilePath { get; set; } // For larger files stored on the file system

        //public string md5{get;set;}
    }
    public enum StorageType
    {
        WebLink,
        LocalLink,
        Database,
    }
}
