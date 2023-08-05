using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
    /// <summary>
    /// 添加新的课程
    /// </summary>
    public class AddCourse
    {
        [Required(ErrorMessage ="课程号不能为空")]
        public string CourseNumber { get; set; }
        [Required(ErrorMessage = "课程名称不能为空")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "课程专业不能为空")]
        public string DepartmentTypeId { get; set; }
        public List<string> UsersId { get; set; }
    }
}