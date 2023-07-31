namespace FlowLearningPlatform.Models.Form
{
    public class AddCourse
    {
        public string CourseNumber { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string DepartmentTypeId { get; set; }

        public List<string> UsersId { get; set; }
    }
}