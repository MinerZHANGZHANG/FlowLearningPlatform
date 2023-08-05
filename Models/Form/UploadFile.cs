namespace FlowLearningPlatform.Models.Form
{
    /// <summary>
    /// 上传文件信息
    /// </summary>
    public class UploadFile
    {
        public Guid UserId { get; set; }
        public FilePurposeType FilePurposeType { get; set; }
        public List<BrowserFile> browserFiles { get; set; }
    }

    /// <summary>
    /// 浏览器文件信息
    /// </summary>
    public class BrowserFile
    {
        public string Name { get; set; }
        public string FileType { get; set; } // 使用浏览器提供的MIME类型
        public long Size { get; set; }
        public DateTime LastModified { get; set; }
        public StorageType StorageType { get; set; } = StorageType.LocalLink;
        public string Url { get;set; }
    }
}
