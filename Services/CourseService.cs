using FlowLearningPlatform.Models;
using FlowLearningPlatform.Models.Form;

namespace FlowLearningPlatform.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync(int pageIndex,int pageCount);
        Task<ServiceResponse<Course>> GetAllByIdAsync(Guid courseId);
        Task<ServiceResponse<List<Course>>> GetByUserIdAsync(string UserId);
        Task<ServiceResponse<string>> AddAsync(AddCourse addCourse);

    }

    public class CourseService : ICourseService
    {
        private readonly DataContext _context;
        private readonly ILogger<CourseService> _logger;

        private bool isRun = false;
        public CourseService(DataContext context,ILogger<CourseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Course>> GetAllAsync(int pageIndex=0, int pageCount=10)
        {
            var result = await _context.Courses
                .OrderBy(x => x.CourseId)
                .Skip(pageIndex*pageCount)
                .Take(pageCount)
                .ToListAsync();
            return result;
        }

        public async Task<ServiceResponse<string>> AddAsync(AddCourse addCourse)
        {
            ServiceResponse<string> response = new() { Success = false };

            if(isRun)
            {
                response.Message = "已经有一个添加任务了";
                return response;
            }
            try
            {
                isRun = true;
                Guid courseId = Guid.NewGuid();
                Course course = new Course()
                {
                    CourseId = courseId,
                    Name = addCourse.Name,
                    CourseNumber = addCourse.CourseNumber,
                    DepartmentTypeId = Guid.Parse(addCourse.DepartmentTypeId),
                    Description = addCourse.Description,
                };

                List<UserCourse> userCourses = new();
                foreach (var userId in addCourse.UsersId)
                {
                    userCourses.Add(new UserCourse() { UserId = Guid.Parse(userId), CourseId = courseId });
                }

                await _context.Courses.AddAsync(course);
                await _context.AddRangeAsync(userCourses);
                await _context.SaveChangesAsync();

                isRun = false;

                response.Success=true;
                return response;

            }
            catch (Exception ex)
            {
                isRun = false;
                _logger.LogError(ex.Message);
                response.Message = "发生错误请联系管理员";
                return response;
            }
           
        }

        public async Task<ServiceResponse<List<Course>>> GetByUserIdAsync(string userId)
        {
            ServiceResponse<List<Course>> response = new() { Success = false ,Data=new List<Course>()};

            if (isRun)
            {
                response.Message = "已经有一个添加任务了";
                return response;
            }
            try
            {
                isRun = true;
                
                Guid uid= Guid.Parse(userId);
                var user = await _context.Users
                     .AsNoTracking()
                     .Include(u => u.Courses)
                     .FirstOrDefaultAsync(u => u.UserId == uid);                  
                    
                response.Data = user.Courses;

                isRun = false;

                response.Success = true;
                return response;

            }
            catch (Exception ex)
            {
                isRun = false;
                _logger.LogError(ex.Message);
                response.Message = "发生错误请联系管理员";
                return response;
            }
        }

		public async Task<ServiceResponse<Course>> GetAllByIdAsync(Guid courseId)
		{
			ServiceResponse<Course> response = new() { Success = false, Data = new () };

			if (isRun)
			{
				response.Message = "已经有一个添加任务了";
                return response;
			}
			try
			{
				isRun = true;

				var course=await _context.Courses.FindAsync(courseId);
                if (course != null)
                {
					response.Success = true;
					response.Data = course;
				}

				isRun = false;
				return response;

			}
			catch (Exception ex)
			{
				isRun = false;
				_logger.LogError(ex.Message);
				response.Message = "发生错误请联系管理员";
				return response;
			}
		}
	}
}
