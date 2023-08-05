namespace FlowLearningPlatform.Models.DTO
{
    /// <summary>
    /// 用户和课程的关系
    /// </summary>
    public class UserWithCourse
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public Guid CourseId { get; set; }
		public bool IsInCourse { get;set; } // 用户是否在这个课程中
	}
}
