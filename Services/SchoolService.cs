namespace FlowLearningPlatform.Services
{
    public interface ISchoolService
    {
        Task<List<SchoolType>> GetAllAsync();
       
    }

    public class SchoolService : ISchoolService
    {
        private readonly DataContext _context;

        public SchoolService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SchoolType>> GetAllAsync()
        {
            var result = await _context.SchoolTypes.OrderBy(x => x.Name).ToListAsync();
            return result;
        }
      
    }
}
