namespace FlowLearningPlatform.Services
{
    public interface IAnnouncementService
    {
        Task<int> GetCountAsync();
        Task<List<Announcement>> GetAllAsync(int pageIndex, int pageSize);
        Task<Announcement> GetByIdAsync(Guid id);
    }
    public class AnnouncementService:IAnnouncementService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly ILogger<AnnouncementService> _logger;

        public AnnouncementService(IDbContextFactory<DataContext> dbContextFactory,ILogger<AnnouncementService> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public Task<List<Announcement>> GetAllAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Announcement> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
