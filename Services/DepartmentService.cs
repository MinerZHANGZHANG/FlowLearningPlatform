using System.Collections.Generic;

namespace FlowLearningPlatform.Services
{
    public interface IDepartmentService
    {
        Task<List<DepartmentType>> GetAllAsync();
        Task<List<DepartmentType>> GetDepartmentBySchool(SchoolType schoolType);
    }

    public class DepartmentService: IDepartmentService
    {
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public DepartmentService(IDbContextFactory<DataContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<DepartmentType>> GetAllAsync()
        {
            List<DepartmentType> result;
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                result = await context.DepartmentTypes.OrderBy(x => x.Name).ToListAsync();
            }
           
            return result;
        }

        public async Task<List<DepartmentType>> GetDepartmentBySchool(SchoolType schoolType)
        {
            List<DepartmentType> result;
            using (var context = await _dbContextFactory.CreateDbContextAsync())
            {
                result = await context.DepartmentTypes
                .Where(d => d.SchoolId.Equals(schoolType.SchoolTypeId))
                .ToListAsync();
            }
           
            return result;
        }
    }


}
