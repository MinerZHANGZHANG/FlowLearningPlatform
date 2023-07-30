namespace FlowLearningPlatform.Services
{
    public interface IRoleService
    {
        Task<List<RoleType>> GetAllAsync();

    }

    public class RoleService : IRoleService
    {
        private readonly DataContext _context;

        public RoleService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RoleType>> GetAllAsync()
        {
            var result = await _context.RoleTypes.OrderBy(x => x.Name).ToListAsync();
            return result;
        }

    }
}
