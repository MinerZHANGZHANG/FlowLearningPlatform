
using FlowLearningPlatform.Models.Form;

namespace FlowLearningPlatform.Services
{
    public interface ISubmissionService
    {
        public Task<ServiceResponse<Submission>> AddSubmissionsAsync(SubmissonInfo submissonInfo);
        /// <summary>
        /// 根据作业id获取对应的提交
        /// </summary>
        /// <param name="AssignmentId">作业Id</param>
        /// <param name="isAll">是否获取全部提交，为false时只获取最新的提交</param>
        /// <returns></returns>
        public Task<ServiceResponse<List<Submission>>> GetByAssignmentIdAsync(Guid AssignmentId,bool isAll=false);
        public Task<ServiceResponse<Submission>> GradeSubmissionAsync(Guid SubmissionId,double score);
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
                // 对于已经提交过的作业新增而不是覆盖
                Submission submission = new()
                {
                    SubmissionId = Guid.NewGuid(),
                    SubmissionTime = DateTime.Now,
                    AssignmentId = submissonInfo.AssignmentId,
                    StudentId = submissonInfo.StudentId,
                    RichText = submissonInfo.RichText,
                    FileSetId = submissonInfo.FileSetId,
                };

                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    // 提交前判断之前是否提交过作业
                    int past = await context.Submissions
                        .AsNoTracking()
                        .Where(s => s.AssignmentId == submissonInfo.AssignmentId && s.StudentId == submissonInfo.StudentId)
                        .CountAsync();

                    int  submissionCount = past+1;
                                         
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

        public async Task<ServiceResponse<List<Submission>>> GetByAssignmentIdAsync(Guid assignmentId,bool isAll=false)
        {
            ServiceResponse<List<Submission>> response = new() { Success = false };
            try
            {              
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    List<Submission> submissions;
                    if (isAll)
                    {
                        // 获取所有提交
                        submissions = await context.Submissions
                        .AsNoTracking()
                        .Include(a => a.Student)
                        .Where(s => s.AssignmentId == assignmentId)                      
                        .ToListAsync();
                    }
                    else
                    {
                        // 只获取最新的提交
                        submissions=await context.Submissions
                        .AsNoTracking()
                        .Include(a=>a.Student)
                        .Where(s=>s.AssignmentId==assignmentId)
                        .GroupBy(s => s.StudentId) 
                        .Select(group => group.OrderByDescending(submission => submission.SubmissionCount).First())
                        .ToListAsync();
                    }
                    
                  
                    response.Success = true;
                    response.Data = submissions;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<Submission>> GradeSubmissionAsync(Guid SubmissionId, double score)
        {
            ServiceResponse<Submission> response = new() { Success = false };
            try
            {                
                using (var context = await _dbContextFactory.CreateDbContextAsync())
                {
                    var submission = await context.Submissions.FindAsync(SubmissionId);
                    if(submission != null)
                    {
                        submission.IsGrade = true;
                        submission.Score = (decimal)score;

                        await context.SaveChangesAsync();

                        response.Success = true;
                        response.Data = submission;
                    }
                    else
                    {
                        response.Message = "没有找到对应编号的作业";
                    }
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
