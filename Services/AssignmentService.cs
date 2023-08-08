
namespace FlowLearningPlatform.Services
{
	public interface IAssignmentService
	{
		// 获取作业信息
		Task<List<Assignment>> GetAllAsync();
		public Task<ServiceResponse<Assignment>> GetByIdAsync(Guid assignmentId);
		public Task<ServiceResponse<List<Assignment>>> GetAllByCourseIdAsync(Guid CourseId);
		public Task<ServiceResponse<List<StudentAssignmentState>>> GetStateByStudentCourseAsync(Guid studentId,Guid courseId);
		public Task<ServiceResponse<List<TeacherAssignmentState>>> GetStateByTeacherCourseAsync(Guid teacherId,Guid courseId);

		public Task<ServiceResponse<List<Assignment>>> GetLastByUserIdAsync(Guid userId,int number=10);


		// 对作业进行增删改
        public Task<ServiceResponse<Assignment>> AddAssignment(PublishHomework publishHomework);
		public Task<ServiceResponse<Assignment>> UpdateAssignment(Assignment assignment);
		public Task<ServiceResponse<bool>> DeleteAssignment(Guid assignmentId);
	}

	public class AssignmentService : IAssignmentService
	{
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly ILogger<AssignmentService> _logger;

		public AssignmentService(IDbContextFactory<DataContext> dbContextFactory,ILogger<AssignmentService> logger)
		{
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public async Task<ServiceResponse<Assignment>> AddAssignment(PublishHomework publishHomework)
        {
			ServiceResponse<Assignment> response = new() { Success = false};
			try
			{
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    Assignment assignment = new Assignment()
                    {
                        AssignmentId = Guid.NewGuid(),

                        Title = publishHomework.Title,
                        Description = publishHomework.Description,
                        UpdateTime = publishHomework.StartTime,
                        Deadline = publishHomework.DeadLine,

                        CourseId = publishHomework.CourseId,

                        FileSetId = publishHomework.FileSetId,

                        Submissions = new(),
                        AutoRename = publishHomework.AutoRename,
                    };

					await context.Assignments.AddAsync(assignment);
					await context.SaveChangesAsync();

					response.Success = true;
					response.Data = assignment;
                }
            }
			catch(Exception ex) 
			{
				_logger.LogError(ex.Message);
			}
               return response;
        }

        public async Task<ServiceResponse<bool>> DeleteAssignment(Guid assignmentId)
        {
            ServiceResponse<bool> response= new() { Success = false };	
            using (var context = await _dbContextFactory.CreateDbContextAsync())
			{
				var assignment = await context.Assignments.FindAsync(assignmentId);
				if (assignment != null)
				{
					assignment.IsHiding = true;
					response.Success = true;
                }
				else
				{
					response.Message="不存在的作业编号";
				}
			}
			return response;
        }

        public async Task<List<Assignment>> GetAllAsync()
		{
			List<Assignment> result=new();

            using (var context =await _dbContextFactory.CreateDbContextAsync())
			{
                result = await context.Assignments.OrderBy(x => x.CourseId).ToListAsync();
            }
			
			return result;
		}

		public async Task<ServiceResponse<List<Assignment>>> GetAllByCourseIdAsync(Guid CourseId)
		{
			ServiceResponse<List<Assignment>> response = new() { Success = false };

			try
			{
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					var data = await context.Courses
					.AsNoTracking()
					.Include(c => c.Assignments)
					.Where(c => c.CourseId == CourseId)
					.Select(c => c.Assignments)
					.FirstAsync();

                    if (data != null)
                    {
                        response.Success = true;
                        response.Data = data;
					}
					else
					{
                        response.Success = true;
						response.Data = new();
                        response.Message = "该课程编号下没有作业";
					}
                }
					
			}
			catch (Exception ex)
			{
                _logger.LogError(ex.Message);
            }

			return response;
		}

		public async Task<ServiceResponse<Assignment>> GetByIdAsync(Guid assignmentId)
		{
			ServiceResponse<Assignment> response = new() { Success = false };
			using (var context = await _dbContextFactory.CreateDbContextAsync())
			{
				Assignment result=await context.Assignments
					.AsNoTracking()
					.Include(a=>a.Course)
					.FirstAsync(a=>a.AssignmentId == assignmentId);
				if (result != null)
				{
					response.Success = true;
					response.Data = result;
				}
			}
			return response;
		}

		public async Task<ServiceResponse<List<Assignment>>> GetLastByUserIdAsync(Guid userId, int number = 10)
		{
			ServiceResponse<List<Assignment>> response = new() { Success = false };

			try
			{
				using (var context =await _dbContextFactory.CreateDbContextAsync())
				{
					var coursesId = await context.UserCourses
						.AsNoTracking()
						//.Include(uc=>uc.Course)
						.Where(uc => uc.UserId == userId && !uc.Course.IsOver)
						.Select(uc => uc.CourseId)
						.ToListAsync();

					var assignments =await context.Assignments
						.AsNoTracking()
						.Where(a => coursesId.Contains(a.CourseId) && a.Deadline > DateTime.Now)
						.OrderBy(a => a.Deadline)
						.Take(number)
						.ToListAsync();
					
					if (assignments != null&&assignments.Count>0)
					{											
						response.Success = true;
						response.Data = assignments;
						
					}
					else
					{
						response.Message = "你没有待完成的作业";
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return response;
		}


		public async Task<ServiceResponse<List<StudentAssignmentState>>> GetStateByStudentCourseAsync(Guid studentId, Guid courseId)
		{
			ServiceResponse<List<StudentAssignmentState>> response = new() { Success = false };

			try
			{
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					var assignments = await context.Assignments
					.AsNoTracking()
					.Where(a => a.CourseId.Equals(courseId))
					.ToListAsync();

					if (assignments != null)
					{
						List<StudentAssignmentState> assignmentStates = new();
						foreach (var assignment in assignments)
						{
							var submission = await context.Submissions
								.AsNoTracking()
								.FirstOrDefaultAsync(s => s.AssignmentId == assignment.AssignmentId && s.StudentId == studentId);
							int submissionCount = 0;
							Guid submissionGuid = Guid.Empty;
							if (submission != null)
							{
								submissionCount = submission.SubmissionCount;
								submissionGuid = submission.SubmissionId;
							}

							StudentAssignmentState temp = new()
							{
								AssignmentId = assignment.AssignmentId,
								CourseId = assignment.CourseId,
								Title = assignment.Title,
								Description = assignment.Description,
								UpdateTime = assignment.UpdateTime,
								Deadline = assignment.Deadline,

								HasFileSet = assignment.FileSetId!=null,
								FileSetId = assignment.FileSetId??Guid.Empty,

								SubmissionCount = submissionCount,
								SubmissionId = submissionGuid,
							};

							assignmentStates.Add(temp);
						}

						if (assignmentStates.Any())
						{
							response.Success = true;
							response.Data = assignmentStates;
						}
					}
					else
					{
						response.Message = "这门课程尚未发布作业";
					}
				}
			}
			catch (Exception ex)
			{
                _logger.LogError(ex.Message);
            }

			return response;
		}

		public async Task<ServiceResponse<List<TeacherAssignmentState>>> GetStateByTeacherCourseAsync(Guid teacherId, Guid courseId)
		{
			ServiceResponse<List<TeacherAssignmentState>> response = new() { Success = false };

			try
			{
				using (var context = await _dbContextFactory.CreateDbContextAsync())
				{
					var assignments = await context.Assignments
					.AsNoTracking()
					.Where(a => a.CourseId.Equals(courseId))
					.ToListAsync();

					if (assignments != null)
					{
						List<TeacherAssignmentState> assignmentStates = new();
						foreach (var assignment in assignments)
						{
							int submissionCount = await context.Submissions
								.AsNoTracking()
								.Where(s => s.AssignmentId == assignment.AssignmentId)
								.GroupBy(s => s.StudentId)
								.CountAsync();

							TeacherAssignmentState temp = new()
							{
								AssignmentId = assignment.AssignmentId,
								CourseId = assignment.CourseId,
								Title = assignment.Title,
								Description = assignment.Description,
								UpdateTime = assignment.UpdateTime,
								Deadline = assignment.Deadline,

								HasFileSet = assignment.FileSetId != null,
								FileSetId = assignment.FileSetId ?? Guid.Empty,

								SubmissionCount = submissionCount,															
							};

							assignmentStates.Add(temp);
						}

						if (assignmentStates.Any())
						{
							response.Success = true;
							response.Data = assignmentStates;
						}
					}
					else
					{
						response.Message = "这门课程尚未发布作业";
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return response;
		}

		public async Task<ServiceResponse<Assignment>> UpdateAssignment(Assignment assignment)
        {
            ServiceResponse<Assignment> response = new() { Success = false };
            try
            {
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
					// TODO update
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
