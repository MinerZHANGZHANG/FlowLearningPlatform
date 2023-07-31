namespace FlowLearningPlatform.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetByDepartmentIdAsync(string departmentId);

    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(DataContext context,ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var result = await _context.Users.OrderBy(x => x.Name).ToListAsync();
            return result;
        }

        public async Task<List<User>> GetByDepartmentIdAsync(string departmentId)
        {
            try
            {
                Guid id=Guid.Parse(departmentId); 
                var result = await _context.Users
                    .AsNoTracking()
                    .Where(u=>u.DepartmentTypeId.Equals(id))
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
