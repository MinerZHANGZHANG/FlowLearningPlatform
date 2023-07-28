using Microsoft.Identity.Client.Extensions.Msal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowLearningPlatform.Models
{
    public class FileData
    {
        [Key]
        public Guid FileDataId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Suffix { get; set; }
        public long Size { get; set; }
        public DateTime UploadTime { get; set; }

        [ForeignKey(nameof(FileSet))]
        public Guid FileSetId { get; set; }
        public FileSet FileSet { get; set; }

        public StorageType StorageType { get; set; }
        public byte[]? FileContent { get; set; } // For smaller files stored in the database
        public string? FilePath { get; set; } // For larger files stored on the file system

    }
    public enum StorageType
    {
        WebLink,
        LocalLink,
        Database,
    }
}
