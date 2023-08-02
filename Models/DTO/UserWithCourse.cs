namespace FlowLearningPlatform.Models.DTO
{
	public class UserWithCourse
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public Guid CourseId { get; set; }
		public bool IsInCourse { get;set; }
	}
}
