using Azure;
using FlowLearningPlatform.Models;
using FlowLearningPlatform.Models.Form;
using System.Collections.Generic;

namespace FlowLearningPlatform.Services
{
    public interface ICourseService
    {
        Task<int> GetCount();
        Task<int> GetUserCount(Guid courseId, bool onlyStudent = true);
        Task<List<Course>> GetAllAsync(int pageIndex,int pageSize);
        Task<ServiceResponse<Course>> GetByIdAsync(Guid courseId);
        Task<ServiceResponse<List<Course>>> GetByUserIdAsync(string UserId);
        Task<ServiceResponse<string>> AddAsync(AddCourse addCourse);
        Task<ServiceResponse<Course>> UpdateAsync(Course updateCourse);
        Task<bool> DeleteByIdAsync(Guid courseId);

    }

    public class CourseService : ICourseService
    {
		private readonly IDbContextFactory<DataContext> _dbContextFactory;
		private readonly ILogger<CourseService> _logger;

        public CourseService(IDbContextFactory<DataContext> dbContextFactory,ILogger<CourseService> logger)
        {
			_dbContextFactory = dbContextFactory;
			_logger = logger;
        }

        /// <summary>
        /// 分页获取课程信息
        /// </summary>
        /// <param name="pageIndex">页数</param>
        /// <param name="pageCount">每页内容量</param>
        /// <returns></returns>
        public async Task<List<Course>> GetAllAsync(int pageIndex=0, int pageCount=10)
        {
			List<Course> result = new();

			using (var _context=await _dbContextFactory.CreateDbContextAsync())
            {
				result = await _context.Courses
				.AsNoTracking()
				.Include(c => c.DepartmentType)
				.OrderBy(x => x.CourseId)
				.Skip(pageIndex * pageCount)
				.Take(pageCount)
				.ToListAsync();
			}
				
            return result;
        }

        /// <summary>
        /// 添加课程
        /// </summary>
        /// <param name="addCourse"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> AddAsync(AddCourse addCourse)
        {
            ServiceResponse<string> response = new() { Success = false };

            try
            {
                Guid courseId = Guid.NewGuid();
                Course course = new()
                {
                    CourseId = courseId,
                    Name = addCourse.Name,
                    CourseNumber = addCourse.CourseNumber,
                    DepartmentTypeId = Guid.Parse(addCourse.DepartmentTypeId),
                    Description = addCourse.Description,
                };

                List<UserCourse> userCourses = new();
                if(addCourse.UsersId != null)
                {
					foreach (var userId in addCourse.UsersId)
					{
						userCourses.Add(new UserCourse() { UserId = Guid.Parse(userId), CourseId = courseId });
					}
				}
                
                using(var _context = await _dbContextFactory.CreateDbContextAsync())
                {
					await _context.Courses.AddAsync(course);
					await _context.AddRangeAsync(userCourses);
					await _context.SaveChangesAsync();
				}

                response.Success=true;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = "发生错误请联系管理员";
                return response;
            }      
        }

        /// <summary>
        /// 从用户编号获取相关的课程列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<Course>>> GetByUserIdAsync(string userId)
        {
            ServiceResponse<List<Course>> response = new() { Success = false ,Data=new List<Course>()};

            try
            {
                Guid uid= Guid.Parse(userId);
				using (var _context = await _dbContextFactory.CreateDbContextAsync())
                {
					var user = await _context.Users
					 .AsNoTracking()
					 .Include(u => u.Courses)
					 .FirstOrDefaultAsync(u => u.UserId == uid);
                    if (user != null)
                    {
                        response.Success = true;
                        response.Data = user.Courses;
                    }                  
				}              
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = "发生错误请联系管理员";
                return response;
            }
        }

        /// <summary>
        /// 根据编号获取课程信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
		public async Task<ServiceResponse<Course>> GetByIdAsync(Guid courseId)
		{
			ServiceResponse<Course> response = new() { Success = false, Data = new () };

			try
			{
				using (var _context = await _dbContextFactory.CreateDbContextAsync())
                {
					var course = await _context.Courses
                        .AsNoTracking()
					    .Include(c => c.DepartmentType)
					    .ThenInclude(d => d.SchoolType)
					    .FirstOrDefaultAsync(c => c.CourseId.Equals(courseId));
					if (course != null)
					{
						response.Success = true;
						response.Data = course;
                    }
                    else
                    {
                        response.Message = "未查询对应编号的课程";
                    }
				}
				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				response.Message = "发生错误请联系管理员";
				return response;
			}
		}

        /// <summary>
        /// 统计课程数量
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCount()
        {
            int result=0;
            using(var _context = await _dbContextFactory.CreateDbContextAsync())
            {
                result=await _context.Courses.CountAsync();
            }
            return result;
        }

        /// <summary>
        /// 统计某个课程的相关用户数量
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
		public async Task<int> GetUserCount(Guid courseId,bool onlyStudent=true)
		{
            int result = 0;

			using (var _context = await _dbContextFactory.CreateDbContextAsync())
			{
                if (onlyStudent)
                {
                    var course = await _context.Courses
						.AsNoTracking()
						.Include(c => c.Users)
						.FirstOrDefaultAsync(c => c.CourseId == courseId);
                    if (course != null)
                    {
						// 或许用户身份也应该改成枚举类型。。
						Guid TeacherGuid = Guid.Parse("9882CD39-3318-4001-913D-9D6F67A0458C");
						result = course.Users.Count(u => u.RoleTypeId !=TeacherGuid);
					}					
                }
                else
                {
                    result = await _context.UserCourses
                        .AsNoTracking()
                        .CountAsync(uc => uc.CourseId == courseId);                    
				}	
			}
			return result;
		}

        public async Task<ServiceResponse<Course>> UpdateAsync(Course updateCourse)
        {
            ServiceResponse<Course> response = new() { Success = false };
            try
            {
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    Course? course = await context.Courses.FindAsync(updateCourse.CourseId);
                    if(course==null)
                    {
                        response.Message = "不存在的课程编号";
                        return response;
                    }
                    else
                    {
                        course.CourseNumber = updateCourse.CourseNumber;
                        course.Name = updateCourse.Name;
                        course.Description = updateCourse.Description;
                        course.DepartmentTypeId = updateCourse.DepartmentTypeId;                    
                        await context.SaveChangesAsync();
                        response.Data = course;
                        response.Success = true;
                    }                   
                }

                response.Success = true;
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Message = "发生错误请联系管理员";
                return response;
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid courseId)
        {
            bool isSuccess = false;
            try
            {
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    var course =await context.Courses.FindAsync(courseId);
                    if(course==null) 
                    {
                        return isSuccess;
                    }
                  context.Courses.Remove(course);
                  await  context.SaveChangesAsync();
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return isSuccess;
        }
    }
}
