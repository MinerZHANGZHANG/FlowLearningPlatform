using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlowLearningPlatform.Services
{
	public interface IAssignmentService
	{
		Task<List<Assignment>> GetAllAsync();
		public Task<ServiceResponse<List<Assignment>>> GetByCourseIdAsync(Guid CourseId);
		public Task<ServiceResponse<List<StudentAssignmentState>>> GetStateByUserCourseAsync(Guid Student,Guid CourseId);

	}

	public class AssignmentService : IAssignmentService
	{
		private readonly DataContext _context;
		private bool _isRun = false;

		public AssignmentService(DataContext context)
		{
			_context = context;
		}

		public async Task<List<Assignment>> GetAllAsync()
		{
			var result = await _context.Assignments.OrderBy(x => x.CourseId).ToListAsync();
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
					var data = await _context.Courses
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
					var assignments=await _context.Assignments
						.AsNoTracking()
						.Where(a=>a.CourseId.Equals(courseId))
						.ToListAsync();

					if (assignments != null)
					{
						List<StudentAssignmentState> assignmentStates = new();
						foreach (var assignment in assignments)
						{
							var submission =await _context.Submissions
								.AsNoTracking()
								.FirstOrDefaultAsync(s => s.Assignmentd == assignment.AssignmentId && s.StudentId == studentId);
							int submissionCount = 0;
							Guid submissionGuid= Guid.Empty;
							if (submission!= null)
							{
								submissionCount=submission.SubmissionCount;
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

								HasFileSet = assignment.FileSetId.Equals(Guid.Empty),
								FileSetId = assignment.FileSetId,

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
				catch (Exception ex)
				{

				}
				_isRun = false;

			}

			return response;
		}
	}
}
