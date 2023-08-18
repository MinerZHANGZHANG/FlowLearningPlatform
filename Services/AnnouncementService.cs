using System.Collections.Generic;
using System.Drawing.Printing;

namespace FlowLearningPlatform.Services
{
    public interface IAnnouncementService
    {
        Task<int> GetCountAsync();
        Task<List<Announcement>> GetAllLatestAsync(int pageIndex, int pageSize);
        Task<Announcement?> GetByIdAsync(Guid id);
        Task<List<Announcement>> GetByAuthorId(Guid authorId,int maxSize=10);
        Task<ServiceResponse<Announcement>> AddAsync(AddAnnouncement addAnnouncement);
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

        public async Task<ServiceResponse<Announcement>> AddAsync(AddAnnouncement addAnnouncement)
        {
            ServiceResponse<Announcement> response = new() { Success = false };
            try
            {
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    Announcement announcement = new Announcement()
                    {
                        AnnouncementId = Guid.NewGuid(),
                        Title = addAnnouncement.Title,
                        SubTitle = addAnnouncement.SubTitle,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        HtmlText = addAnnouncement.HtmlText,
                        Icon = addAnnouncement.Icon,
                        UserId = addAnnouncement.UserId,
                    };

                    await context.Announcements.AddAsync(announcement);
                    await context.SaveChangesAsync();
                    response.Success = true;
                    response.Data = announcement;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }

        public async Task<List<Announcement>> GetAllLatestAsync(int pageIndex, int pageSize)
        {
            
            List<Announcement> result = new();
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                //// 使用Linq
                //result.AddRange(
                //    await context.Announcements
                //    .AsNoTracking()
                //    .OrderByDescending(a => a.UpdateTime)
                //    .Skip(pageIndex * pageSize)
                //    .Take(pageSize)
                //    .ToListAsync());
                // 使用设置好的存储过程
                result.AddRange(
                    await context.Announcements
                    .FromSql($"GetAnnouncements {pageIndex},{pageSize}")
                    .ToListAsync());
            }


            return result;
        }

		public async Task<List<Announcement>> GetByAuthorId(Guid authorId, int maxSize = 10)
		{
			List<Announcement> result = new();
			using (var context = await _dbContextFactory.CreateDbContextAsync())
			{
				result.AddRange(
					await context.Announcements
					.AsNoTracking()
                    .Include(a=>a.User)
					.OrderByDescending(a => a.UpdateTime)
					.Where(a=>a.UserId == authorId)
					.Take(maxSize)
					.ToListAsync());
			}
			return result;
		}

		public async Task<Announcement?> GetByIdAsync(Guid id)
        {
            Announcement? result;
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                result=await context.Announcements
                    .AsNoTracking()
                    .Include(a=>a.User)
                    .FirstOrDefaultAsync(a=>a.AnnouncementId == id);
            }
            return result;
        }

        public async Task<int> GetCountAsync()
        {
            int result = 0;
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                result = await context.Announcements.CountAsync();
            }
            return result;
        }
    }
}
