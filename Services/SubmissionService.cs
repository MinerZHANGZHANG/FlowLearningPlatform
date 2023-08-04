using Microsoft.EntityFrameworkCore;

namespace FlowLearningPlatform.Services
{
    public interface ISubmissionService
    {
        public Task<ServiceResponse<Submission>> AddSubmissionsAsync(SubmissonInfo submissonInfo);
    }

    public class SubmissionService : ISubmissionService
    {
        private readonly ILogger<SubmissionService> _logger;
        private readonly IDbContextFactory<DataContext> _dbContextFactory;

        public SubmissionService(ILogger<SubmissionService> logger, IDbContextFactory<DataContext> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        public async Task<ServiceResponse<Submission>> AddSubmissionsAsync(SubmissonInfo submissonInfo)
        {
            ServiceResponse<Submission> response = new() { Success = false };
            try
            {
                Submission submission = new()
                {
                    SubmissionId = Guid.NewGuid(),
                    SubmissionTime = DateTime.Now,
                    Assignmentd = submissonInfo.AssignmentId,
                    StudentId = submissonInfo.StudentId,
                    RichText = submissonInfo.RichText,
                    FileSetId = submissonInfo.FileSetId,
                };

                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    int submissionCount = 0;
                    var last = await context.Submissions
                        .AsNoTracking()
                        .Where(s => s.Assignmentd == submissonInfo.AssignmentId && s.StudentId == submissonInfo.StudentId)
                        .FirstOrDefaultAsync();

                    if(last != null)
                    {
                        submissionCount = last.SubmissionCount+1;
                    }
                       


                    submission.SubmissionCount = submissionCount;
                    
                    await context.Submissions.AddAsync(submission);
                    await context.SaveChangesAsync();

                    response.Success = true;
                    response.Data = submission;
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
