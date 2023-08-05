namespace FlowLearningPlatform.Models.DTO
{
	public class TeacherAssignmentState
	{
		public Guid AssignmentId { get; set; }
		public Guid CourseId { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime UpdateTime { get; set; }
		public DateTime Deadline { get; set; }

		public bool HasFileSet { get; set; }
		public Guid FileSetId { get; set; }

		public int SubmissionCount { get; set; }
	}
}
