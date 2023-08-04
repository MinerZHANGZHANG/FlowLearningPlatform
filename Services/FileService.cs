namespace FlowLearningPlatform.Services
{
    public interface IFileService 
    {
        public Task<ServiceResponse<FileSet>> UploadFilesAsync(UploadFile uploadFile);
    }

    public class FileService:IFileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public FileService(ILogger<FileService> logger,IDbContextFactory<DataContext> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ServiceResponse<FileSet>> UploadFilesAsync(UploadFile uploadFile)
        {
            ServiceResponse<FileSet> response = new() { Success = false };

            try
            {
                FileSet fileSet = new()
                {
                    FileSetId = Guid.NewGuid(),
                    FilePurposeType = uploadFile.FilePurposeType,
                    UploadUserId = uploadFile.UserId,
                };

                using (var context=await _dbContextFactory.CreateDbContextAsync())
                {
                   await context.FileSets.AddAsync(fileSet);
                   await context.SaveChangesAsync();

                    List<FileData> files = new();

                    foreach (var file in uploadFile.browserFiles)
                    {
                        FileData fileData = new()
                        {
                            FileDataId = Guid.NewGuid(),
                            Name = file.Name,
                            Suffix = file.FileType,
                            Size = file.Size,
                            UploadTime = DateTime.Now,

                            FileSetId = fileSet.FileSetId,
                            StorageType = file.StorageType,

                            FilePath = file.Url
                        };
                        files.Add(fileData);
                    }

                    await context.FileDatas.AddRangeAsync(files);
                    await context.SaveChangesAsync();

                    fileSet.Files=files;
                    response.Success = true;
                    response.Data=fileSet;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return response;
        }
    }
}
