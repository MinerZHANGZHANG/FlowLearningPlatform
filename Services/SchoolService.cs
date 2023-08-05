
namespace FlowLearningPlatform.Services
{
    public interface ISchoolService
    {
        Task<List<SchoolType>> GetAllAsync();
    }

    public class SchoolService : ISchoolService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public SchoolService(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<SchoolType>> GetAllAsync()
        {
            List<SchoolType> result;
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                result = await context.SchoolTypes
                    .AsNoTracking()
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
            return result;
        }
    }
}
