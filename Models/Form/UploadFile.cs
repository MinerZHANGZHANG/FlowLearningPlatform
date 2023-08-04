
namespace FlowLearningPlatform.Models.Form
{
    public class UploadFile
    {
        public Guid UserId { get; set; }
        public FilePurposeType FilePurposeType { get; set; }
        public List<BrowserFile> browserFiles { get; set; }
    }

    public class BrowserFile
    {
        public string Name { get; set; }
        public string FileType { get; set; }
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
        public StorageType StorageType { get; set; } = StorageType.LocalLink;
        public string Url { get;set; }
    }
}
