using System.ComponentModel.DataAnnotations;

namespace FlowLearningPlatform.Models.Form
{
	/// <summary>
	/// 用户注册的第二部分，填写基本信息的表单模型
	/// </summary>
	public class RegisterSecondStep
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string DepartmentTypeId { get; set; }
        [Required]
        public string Password { get;set; }
		[Required,Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
	}
}
