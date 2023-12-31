﻿

namespace FlowLearningPlatform.Models.DTO
{
	/// <summary>
	/// 学生对于某个作业的提交状态
	/// </summary>
	public class StudentAssignmentState
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
		public Guid SubmissionId { get; set; }


	}
}
