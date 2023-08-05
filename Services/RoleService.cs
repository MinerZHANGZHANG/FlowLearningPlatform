
namespace FlowLearningPlatform.Services
{
    public interface IRoleService
    {
        Task<List<RoleType>> GetAllAsync();
    }

    public class RoleService : IRoleService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public RoleService(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<RoleType>> GetAllAsync()
        {
            List<RoleType> result;
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                result = await context.RoleTypes
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
            return result;
        }
    }
}
