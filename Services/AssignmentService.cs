using FlowLearningPlatform.Models;
using FlowLearningPlatform.Models.Form;
using System.Collections.Generic;

namespace FlowLearningPlatform.Services
{
	public interface IAssignmentService
	{
		Task<List<Assignment>> GetAllAsync();
		public Task<ServiceResponse<List<Assignment>>> GetByCourseIdAsync(Guid CourseId);
		public Task<ServiceResponse<List<StudentAssignmentState>>> GetStateByUserCourseAsync(Guid Student,Guid CourseId);
		public Task<ServiceResponse<Assignment>> AddAssignment(PublishHomework publishHomework);
		public Task<ServiceResponse<Assignment>> UpdateAssignment(Assignment assignment);
		public Task<ServiceResponse<bool>> DeleteAssignment(Guid assignmentId);
	}

	public class AssignmentService : IAssignmentService
	{
        private readonly IDbContextFactory<DataContext> _dbContextFactory;
        private readonly ILogger<AssignmentService> _logger;
        private bool _isRun = false;

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

					await	context.Assignments.AddAsync(assignment);
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

		public async Task<ServiceResponse<List<Assignment>>> GetByCourseIdAsync(Guid CourseId)
		{
			ServiceResponse<List<Assignment>> response = new() { Success = false };

			if (!_isRun)
			{
				_isRun = true;

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
                    }
					
				}
				catch (Exception ex)
				{

				}
				_isRun = false;

			}

			return response;
		}

		public async Task<ServiceResponse<List<StudentAssignmentState>>> GetStateByUserCourseAsync(Guid studentId, Guid courseId)
		{
			ServiceResponse<List<StudentAssignmentState>> response = new() { Success = false };

			if (!_isRun)
			{
				_isRun = true;

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
									.FirstOrDefaultAsync(s => s.Assignmentd == assignment.AssignmentId && s.StudentId == studentId);
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

					}
				}
				catch (Exception ex)
				{

				}
				_isRun = false;

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
