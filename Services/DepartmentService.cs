namespace FlowLearningPlatform.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentType>> GetAllAsync();
        Task<List<DepartmentType>> GetDepartmentBySchool(SchoolType schoolType);
    }

    public class DepartmentService: IDepartmentService
    {
        private readonly DataContext _context;

        public DepartmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentType>> GetAllAsync()
        {
           var result= await _context.DepartmentTypes.OrderBy(x => x.Name).ToListAsync();
            return result;
        }

        public async Task<List<DepartmentType>> GetDepartmentBySchool(SchoolType schoolType)
        {
            var result = await _context.DepartmentTypes
                 .Where(d => d.SchoolId.Equals(schoolType.SchoolTypeId))
                 .ToListAsync();
            return result;
        }
    }


}
