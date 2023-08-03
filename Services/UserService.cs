using FlowLearningPlatform.Models;
using System.Collections.Generic;

namespace FlowLearningPlatform.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetByDepartmentIdAsync(string departmentId);
        Task<List<UserWithCourse>> GetUserWithCourseAsync(Guid courseId);

		Task<ServiceResponse<List<UserWithCourse>>> AddUserToCourse(Guid courseId, List<Guid> usersId);
		Task<ServiceResponse<List<UserWithCourse>>> RemoveUserFromCourse(Guid courseId, List<Guid> usersId);
	}

    public class UserService : IUserService
    {
		private readonly IDbContextFactory<DataContext> _dbContextFactory;
		private readonly ILogger<UserService> _logger;

        public UserService(IDbContextFactory<DataContext> dbContextFactory,ILogger<UserService> logger)
        {
			_dbContextFactory = dbContextFactory;
			_logger = logger;
        }

		public async Task<ServiceResponse<List<UserWithCourse>>> AddUserToCourse(Guid courseId, List<Guid> usersId)
		{
			ServiceResponse<List<UserWithCourse>> response = new() { Success=false,Data=new()};
			try
			{
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					List<UserCourse> userCourses = new();
					foreach (var userId in usersId)
					{
						// 可能需要检测一下输入的用户是否已经在课程中
						UserCourse userCourse = new()
						{
							CourseId = courseId,
							UserId = userId,
						};
						userCourses.Add(userCourse);
					}


					await context.UserCourses.AddRangeAsync(userCourses);
					await context.SaveChangesAsync();
				}

				var result=await GetUserWithCourseAsync(courseId);
				response.Success = true;
				response.Data = result;
			}
			catch	(Exception ex)
			{
				_logger.LogError(ex.Message);
			}
			
			return response;
		}

		public async Task<List<User>> GetAllAsync()
        {
			List<User> result;
			using(var context=await _dbContextFactory.CreateDbContextAsync())
			{
				result = await context.Users.OrderBy(x => x.Name).ToListAsync();
			}
            
            return result;
        }

        public async Task<List<User>> GetByDepartmentIdAsync(string departmentId)
        {
			List<User> result=new();
            try
            {
                Guid id=Guid.Parse(departmentId);
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					result = await context.Users
					.AsNoTracking()
					.Where(u => u.DepartmentTypeId.Equals(id))
					.ToListAsync();
				}
				
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return result;
            }
        }

		public async Task<List<UserWithCourse>> GetUserWithCourseAsync(Guid courseId)
		{
            List<UserWithCourse> result = new();

            try
            {
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					Course? course = await context.Courses
				  .AsNoTracking()
				  .Include(c => c.Users)
				  .FirstOrDefaultAsync(c => c.CourseId == courseId);

					if (course != null)
					{
						List<User> users = await context.Users
							.AsNoTracking()
							.Where(u => u.DepartmentTypeId == course.DepartmentTypeId)
							.ToListAsync();

						foreach (var user in users)
						{
							UserWithCourse userWithCourse = new()
							{
								UserId = user.UserId,
								UserName = user.Name,
								CourseId = courseId,
								IsInCourse = course.Users.Exists(u => u.UserId == user.UserId)
							};
							result.Add(userWithCourse);
						}
					}
				}
				
			}
            catch(Exception ex) 
            {
				_logger.LogError(ex.Message);
			}
          
            return result;
		}

		public async Task<ServiceResponse<List<UserWithCourse>>> RemoveUserFromCourse(Guid courseId, List<Guid> usersId)
		{
			ServiceResponse<List<UserWithCourse>> response = new() { Success = false, Data = new() };
			try
			{
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					List<UserCourse> userCourses = new();
					var currentUserCourses =await context.UserCourses
						.AsNoTracking()
						.Where(uc => uc.CourseId == courseId)
						.ToListAsync();
					foreach (var userId in usersId)
					{
						if (currentUserCourses.Exists(c => c.CourseId == courseId && c.UserId == userId))
						{
							UserCourse userCourse = new()
							{
								CourseId = courseId,
								UserId = userId,
							};
							userCourses.Add(userCourse);
						}					
					}

					context.UserCourses.RemoveRange(userCourses);
					await context.SaveChangesAsync();
				}

				var result = await GetUserWithCourseAsync(courseId);
				response.Success = true;
				response.Data = result;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return response;
		}
	}
}
